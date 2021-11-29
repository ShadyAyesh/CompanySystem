using System;
using System.Security.Cryptography;
using System.Text;

namespace CompanySystem.Application.Common
{
    public static class Security
    {
        public static string Encrypt(string password)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var encoder = new UTF8Encoding();
            var hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));
            var retVal = Convert.ToBase64String(hashedBytes);

            return retVal;
        }
    }
}