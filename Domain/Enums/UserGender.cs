using System.ComponentModel;

namespace TaskManager.Domain.Enums;

public enum UserGender
{
    /// <summary>
    ///     مرد
    /// </summary>
    [Description("مرد")] Male = 1,

    /// <summary>
    ///     زن
    /// </summary>
    [Description("زن")] Female = 2
}