using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace NLayer.Core.Entities
{
    public class PasswordHasher
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public static byte[] HashPassword(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Array.Copy(passwordBytes, saltedPassword, passwordBytes.Length);
                Array.Copy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedPassword = sha256.ComputeHash(saltedPassword);
                return hashedPassword;
            }
        }
        public static string VerifyPassword(string password, byte[] salt, byte[] hashedPassword)
        {
            byte[] inputHash = HashPassword(password, salt);
            if(StructuralComparisons.StructuralEqualityComparer.Equals(inputHash, hashedPassword))
            {
                return password;
            }
            else
            {
                return "sifre aynı degil";
            }
        }

    }
}