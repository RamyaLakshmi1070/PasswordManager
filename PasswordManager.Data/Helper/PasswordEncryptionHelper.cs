using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Helper
{
    public static class PasswordEncryptionHelper
    {
        public static string Encrypt(string plainText) =>
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));

        public static string Decrypt(string encryptedText) =>
            System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encryptedText));

    }

}
