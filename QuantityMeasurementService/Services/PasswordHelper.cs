using System.Security.Cryptography;
using System.Text;

public class PasswordHelper
{
    public static void CreatePasswordHash(string password, out string hash, out string salt)
    {
        using var hmac = new HMACSHA256();

        salt = Convert.ToBase64String(hmac.Key);
        hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var key = Convert.FromBase64String(storedSalt);

        using var hmac = new HMACSHA256(key);

        var computedHash = Convert.ToBase64String(
            hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

        return computedHash == storedHash;
    }
}