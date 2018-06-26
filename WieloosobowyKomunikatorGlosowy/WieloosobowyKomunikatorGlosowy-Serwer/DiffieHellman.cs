using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    public class DiffieHellman
    {
        public byte[] serverPublicKey;
        public byte[] iv;
        byte[] sessionKey;
        ECDiffieHellmanCng server = new ECDiffieHellmanCng();
        private static Random random = new Random();

        public string ipAdress;
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public DiffieHellman()
        {
            server.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            server.HashAlgorithm = CngAlgorithm.Sha256;
            serverPublicKey = server.PublicKey.ToByteArray();
            iv = Encoding.ASCII.GetBytes(RandomString(16));
        }

        public void CreateDeriveKey(string clientPublicKey)
        {
            sessionKey = server.DeriveKeyMaterial(CngKey.Import(Convert.FromBase64String(clientPublicKey), CngKeyBlobFormat.EccPublicBlob));
        }
        public string EncryptMessage(string message)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = sessionKey;
                aes.IV = iv;
                byte[] encryptedMessage;
                using (MemoryStream ciphertext = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Convert.FromBase64String(message.Replace(";", "/"));
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.Close();
                    encryptedMessage = ciphertext.ToArray();
                    return Convert.ToBase64String(encryptedMessage);
                }

            }
        }

        public string DecryptMessage(byte[] message)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = sessionKey;
                aes.IV = iv;
                using (MemoryStream plaintext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(message, 0, message.Length);
                        cs.Close();
                        return Convert.ToBase64String(plaintext.ToArray()).Replace("/",";");
                    }
                }
            }
        }
    }

}

