using System.Security.Cryptography;
using System.Text;

namespace AoSP.Helpers;

public static class Helper
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        return hash;
    }

    public static string GenerateId()
    {
        var builder = new StringBuilder();
        Enumerable
            .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(_ => Guid.NewGuid())
            .Take(12)
            .ToList().ForEach(e => builder.Append(e));
        return builder.ToString();
    }
}