using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy
{
    public class DiffieHellman
    {
        public byte[] clientPublicKey;
        byte[] iv;
        ECDiffieHellmanCng client = new ECDiffieHellmanCng();
        byte[] sessionKey;
        public DiffieHellman()
        {
            client.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            client.HashAlgorithm = CngAlgorithm.Sha256;
            clientPublicKey = client.PublicKey.ToByteArray();
        }

        public void CreateDeriveKey(string serverPublicKey)
        {
            sessionKey = client.DeriveKeyMaterial(CngKey.Import(Convert.FromBase64String(serverPublicKey), CngKeyBlobFormat.EccPublicBlob));
        }

        public void SetIV(string iv)
        {
            this.iv = Encoding.ASCII.GetBytes(iv);
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
                    int mod4 = message.Length % 4;
                    if (mod4 > 0)
                    {
                        message += new string(';', 4 - mod4);
                    }
                    byte[] plaintextMessage = Convert.FromBase64String(message.Replace(";", "/"));
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.Close();
                    encryptedMessage = ciphertext.ToArray();
                    Console.WriteLine(Convert.ToBase64String(encryptedMessage));
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
                        return Convert.ToBase64String(plaintext.ToArray()).Replace("/", ";");
                    }
                }
            }
        }
    }
}
