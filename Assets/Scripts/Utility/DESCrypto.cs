using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Ebleme.Utility {

    public static class DESCrypto {
        public static bool useSecure = true;

        private const int Iterations = 555;

        private static readonly string _key = "G#r_XZdJ2+W>Z$x7";

        private static byte[] GetIV() {
            return Encoding.UTF8.GetBytes(_key);
        }

        public static string Encrypt(string strPlain) {
            if (!useSecure)
                return strPlain;

            try {
                using var des = DES.Create();
                using var rfc = new Rfc2898DeriveBytes(_key, GetIV(), Iterations);
                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(rfc.GetBytes(8), GetIV()), CryptoStreamMode.Write); memoryStream.Write(GetIV(), 0, GetIV().Length);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(strPlain);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            } catch (Exception e) {
                Debug.LogErrorFormat("Encrypt Exception: {0}", e);
                return strPlain;
            }
        }

        public static string Decrypt(string strEncript) {
            if (!useSecure)
                return strEncript;

            try {
                byte[] cipherBytes = Convert.FromBase64String(strEncript);

                byte[] iv = GetIV();

                using var des = DES.Create();
                using var rfc = new Rfc2898DeriveBytes(_key, GetIV(), Iterations);
                using var memoryStream = new MemoryStream(cipherBytes);
                memoryStream.Read(iv, 0, iv.Length);
                using var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(rfc.GetBytes(8), iv), CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);
                return streamReader.ReadToEnd();
            } catch (Exception e) {
                Debug.LogErrorFormat("Decrypt Exception: {0}", e);
                return strEncript;
            }
        }
    }
}
