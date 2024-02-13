using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities.Encryption
{
    public class PasswordEncryption
    {
        public struct Password
        {
            public string Hash { get; set; }
            public string Salt { get; set; }
        }

        public static string CreateHashedPasswordWithSaltFromDb(string inputPassword, string passwordSaltFromDb)
        {
            string inputPasswordHash;
            byte[] data = Encoding.UTF8.GetBytes(inputPassword + passwordSaltFromDb);

            SHA256 sha256hash = SHA256.Create();
            data = sha256hash.ComputeHash(data);

            inputPasswordHash = Convert.ToBase64String(data);

            return inputPasswordHash;
        }

        public static Password CreateHashedPasswordAndSalt(string inputPassword)
        {
            string salt = CreateSalt();

            byte[] data = Encoding.UTF8.GetBytes(inputPassword + salt);

            SHA256 sha256hash = SHA256.Create();

            data = sha256hash.ComputeHash(data);

            string hashedPassword = Convert.ToBase64String(data);

            Password password = new Password
            {
                Hash = hashedPassword,
                Salt = salt
            };

            return password;
        }

        private static string CreateSalt()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] buff = new byte[25];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
