using System.ComponentModel;

namespace TaskManager.Identity;

public enum UserRoles
{
    /// <summary>
    ///     ادمین
    /// </summary>
    [Description("ادمین")] Admin = 1,

    /// <summary>
    ///     مدیر
    /// </summary>
    [Description("مدیر")] Manager = 2,

    /// <summary>
    ///     کارمند
    /// </summary>
    [Description("کارمند")] Employee = 3
}