using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace AoSP.Extensions;

public static class Extension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        ?.GetName() ?? "Неопределенный";
    }
    
    public static string Join(this List<string> words) 
    {
        var sb = new StringBuilder();
            
        for (int i = 0; i < words.Count; i++)
        {
            sb.Append($"{i + 1}: {words[i]} ");
        }
            
        return sb.ToString();
    }
}