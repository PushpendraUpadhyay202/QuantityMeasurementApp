using BusinessLayer.Interfaces;
using System.Security.Cryptography;
using ModelLayer.DTOs;

namespace BusinessLayer.Services
{
    public class PasswordService: IPasswordService
    {
        private const int SaltSize = 16; // 16 bytes
        private const int HashSize = 32; // 32 bytes
        private const int Iterations = 100000;

        public HashPasswordResponseDto HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            return new HashPasswordResponseDto
            {
                HashedPassword = $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}"
            };
        }
        public VerifyPasswordResponseDto VerifyPassword(string inputPassword, string storedHash)
        {
            var parts = storedHash.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] originalHash = Convert.FromBase64String(parts[1]);

            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] inputHash = pbkdf2.GetBytes(HashSize);

            return new VerifyPasswordResponseDto
            {
              IsValid = CryptographicOperations.FixedTimeEquals(inputHash, originalHash)  
            };
        }
    }
}