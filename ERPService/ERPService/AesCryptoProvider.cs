using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ERPService
{
    public class AesCryptoProvider : ICryptor
    {
        public string Decrypt(string strEncryptedText)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var d = GetCryptoTransform(csp, false);
                byte[] output = Convert.FromBase64String(strEncryptedText);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);

                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        public string Encrypt(string strNormalText)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(strNormalText);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

                string encrypted = Convert.ToBase64String(output);

                return encrypted;
            }
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var passWord = "D8903F7F075E4877A6C5F62EC68A2018";
            var salt = "S@1tS@lt";
            byte[] ivArr = (new char[] { '1', 'c', 'b', '2', 'a', '8', '0', 'f', '4', '3', '9', 'j', '7', '3', '9', 't' }).Select(c => (byte)c).ToArray();

            //a random Init. Vector. just for testing
            string iv = "e675f725e675f725";

            // var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passWord), Encoding.UTF8.GetBytes(salt), 65536);
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passWord), ivArr, 65536);

            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(iv);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }
    }
}