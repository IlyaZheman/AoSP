﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AoSP.Extensions;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        ?.GetName() ?? "Неопределенный";
    }
}