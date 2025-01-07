using System.Security.Cryptography;
using System.Text;

namespace Employee.Management.MVC.Utitlies
{
    public class SymmetricEncryptionHelper
    {
        public static string Encrypt(string plainText, string privateKey)
        {
            // Generate a random salt
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive key and IV from the password and salt
            using (var keyDerivation = new Rfc2898DeriveBytes(privateKey, salt, 10000))
            {
                byte[] key = keyDerivation.GetBytes(32); // AES-256 key
                byte[] iv = keyDerivation.GetBytes(16); // AES IV

                using (var aes = new AesManaged { Key = key, IV = iv })
                {
                    var encryptor = aes.CreateEncryptor();
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    return $"{Convert.ToBase64String(encryptedBytes)}:{Convert.ToBase64String(salt)}";
                }
            }
        }
    }

    public static class CommonMessage
    {
        public static string ErrorMessage = "Sorry, we were unable to complete your request. Please contact Support";
    }
}
