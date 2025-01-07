using System.Security.Cryptography;
using System.Text;

namespace Employee.Management.Application.Utilities
{
    public static class SymmetricEncryptionHelper
    {
        public static string Decrypt(string encrytedText, string privateKey)
        {
            var parts = encrytedText.Split(':');

            if (parts.Length != 2)
                throw new ArgumentException("Invalid data format.");

            string encryptedData = parts[0];
            string saltString = parts[1];

            byte[] saltBytes = Convert.FromBase64String(saltString);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            // Derive key and IV from the password and salt
            using (var keyDerivation = new Rfc2898DeriveBytes(privateKey, saltBytes, 10000))
            {
                byte[] key = keyDerivation.GetBytes(32); // AES-256 key
                byte[] iv = keyDerivation.GetBytes(16); // AES IV

                using (var aes = new AesManaged { Key = key, IV = iv })
                {
                    var decryptor = aes.CreateDecryptor();
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
