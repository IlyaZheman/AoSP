using System.Runtime.InteropServices;
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

    public static string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return Path.GetFileNameWithoutExtension(fileName)
            + "_"
            + Guid.NewGuid().ToString().Substring(0, 4)
            + Path.GetExtension(fileName);
    }

    public static byte[] GetByteArrayFromFile(IFormFile file)
    {
        using (var target = new MemoryStream())
        {
            file.CopyTo(target);
            return target.ToArray();
        }
    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags,
        IntPtr hToken, out string pszPath);

    public static void DownloadFileFromByteArray(string fileName, byte[] bytes)
    {
        SHGetKnownFolderPath(new Guid("374DE290-123F-4565-9164-39C4925E467B"), 0, IntPtr.Zero, out var downloads);
        downloads += $"\\{fileName}";
        using (var fileStream = new FileStream(downloads, FileMode.Create))
        {
            fileStream.Write(bytes, 0, bytes.Length);
        }
    }
}