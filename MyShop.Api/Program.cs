using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManager.Application;
using TaskManager.Application.Profiles;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Identity;
using TaskManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Paste your JWT token directly here"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT"
                }
            },
            new string[] { }
        }
    });

    // توضيحات api در swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

#endregion

#region Application

builder.Services.ConfigureApplicationServices();

#endregion

#region Infrastructure

builder.Services.ConfigureInfrastructureServices();

#endregion

#region Identity

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<TaskManagerDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region JWT Authentication

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var db = context.HttpContext.RequestServices.GetRequiredService<TaskManagerDbContext>();

                string rawToken = null;

                if (context.SecurityToken is JwtSecurityToken jwtToken && !string.IsNullOrEmpty(jwtToken.RawData))
                {
                    rawToken = jwtToken.RawData;
                }
                else
                {
                    var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
                    if (!string.IsNullOrEmpty(authHeader) &&
                        authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        rawToken = authHeader.Substring(7).Trim();
                }

                if (string.IsNullOrEmpty(rawToken)) return;

                var now = DateTime.UtcNow;

                var expiredTokens = await db.RevokedTokens
                    .Where(t => t.ExpireAt < now)
                    .ToListAsync();

                if (expiredTokens.Any())
                {
                    db.RevokedTokens.RemoveRange(expiredTokens);
                    await db.SaveChangesAsync();
                }

                var activeRevokedTokens = await db.RevokedTokens
                    .Where(t => t.ExpireAt >= now)
                    .Select(t => t.Token)
                    .ToListAsync();

                var tokenWithoutBearer = rawToken.Trim();

                var tokenWithBearer = "Bearer " + tokenWithoutBearer;

                var isRevoked = activeRevokedTokens.Any(t =>
                {
                    var trimmedToken = t?.Trim() ?? string.Empty;
                    return trimmedToken == tokenWithoutBearer || trimmedToken == tokenWithBearer;
                });

                if (isRevoked) context.Fail("این توکن لغو شده است.");
            }
        };
    });

#endregion

#region Context

builder.Services.AddDbContext<TaskManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

#endregion

#region Mapper

var loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());

var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }, loggerFactory);

var mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

#endregion

var app = builder.Build();

#region RoleSeedData

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    await SeedRoles.Initialize(roleManager, userManager);
}

#endregion

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();