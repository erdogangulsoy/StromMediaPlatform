using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace StromMediaPlatform.Utils
{
    public class Crypto
    {
        public static string GenerateRandomString(int size, bool includeSpecialChars = true)
        {
            // Characters except I, l, O, 1, o, and 0 to decrease confusion when hand typing tokens
            string charSet = null;

            if (includeSpecialChars)
            {
                charSet = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@#$%&()";
            }
            else
            {
                charSet = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            }

            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public static string GenerateRandomNumbers(int size)
        {
            
            string charSet = "0123456789";
           
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public static string GenerateToken(string input, string key)
        {

            String signature = String.Empty;

            using (HMACSHA256 hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                Byte[] dataToHmac = Encoding.UTF8.GetBytes(input);
                signature = Convert.ToBase64String(hmacSha256.ComputeHash(dataToHmac));
            }

            return signature;
        }
    }
}
