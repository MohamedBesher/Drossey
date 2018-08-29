using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Drossey.Admin.Services
{
    public class PinCodeGenerator : IPinCodeGenerator
    {
        //public string Decrypt(string Decrptedkey)
        //{

        //    var random=Decrptedkey.Substring(0, 10);
        //    using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        //    {
        //        // Decrypt the text
        //        byte[] iv = Convert.FromBase64String(ivAsBase64);
        //        byte[] keyBytes = Convert.FromBase64String(keyAsBase64);
        //        byte[] fromBase64ToBytes = Convert.FromBase64String(encryptedTextAsBase64);
        //        var decryptor = aes.CreateDecryptor(keyBytes, iv);
        //        byte[] decryptedBytes = decryptor.TransformFinalBlock(fromBase64ToBytes, 0, fromBase64ToBytes.Length);
        //        Console.WriteLine("Decrypted: {0}", Encoding.UTF8.GetString(decryptedBytes));
        //    }
        //    return 
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encrptedkey"></param>
        /// <returns> inilization Vector - key - digits </returns>
        public Tuple<string, string, string, double> Encrypt(int amount)
        {
            Random r = new Random();
            var encrptedkey = (Math.Pow(10, 9)) + r.Next(9 * (Convert.ToInt32(Math.Pow(10, 9))));

            string ivAsBase64;
            string encryptedTextAsBase64;
            string keyAsBase64;
            string digits;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                byte[] iv = aes.IV;
                ivAsBase64 = Convert.ToBase64String(iv);          
                aes.GenerateKey();
               
                // Base64 the key for storage
                keyAsBase64 = Convert.ToBase64String(aes.Key);
                // Encrypt the text
                byte[] textBytes = Encoding.UTF8.GetBytes($"{keyAsBase64}!@#$%^&*()_+~{amount}");
                var cryptor = aes.CreateEncryptor();
                byte[] encryptedBytes = cryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                encryptedTextAsBase64 = Convert.ToBase64String(encryptedBytes);

                List<char> datalist = new List<char>();
                datalist.AddRange(encryptedTextAsBase64.Select(c => c));

                 digits=String.Concat(datalist.Where(c => char.IsDigit(c))).Substring(0, 5); ;
            }

            return new Tuple<string, string, string,double>(ivAsBase64, keyAsBase64, digits, encrptedkey);


        }

        public string GetCode(int amount,double rand,string ivAsBase64 ,string keyAsBase64)
        {
            string encryptedTextAsBase64;
            string digits;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {

                aes.IV = Convert.FromBase64String(ivAsBase64);


                //Console.WriteLine("Key length: {0}", key.Length);
                byte[] keyBytes = Convert.FromBase64String(keyAsBase64);
                aes.Key = keyBytes;
                // Encrypt the text
                byte[] textBytes = Encoding.UTF8.GetBytes($"{keyAsBase64}!@#$%^&*()_+~{amount}");
                var cryptor = aes.CreateEncryptor();
                byte[] encryptedBytes = cryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                encryptedTextAsBase64 = Convert.ToBase64String(encryptedBytes);
                List<char> datalist = new List<char>();
                datalist.AddRange(encryptedTextAsBase64.Select(c => c));
                digits = String.Concat(datalist.Where(c => char.IsDigit(c))).Substring(0, 5); ;
            }
            return rand.ToString() + digits;
        }
    }


}

