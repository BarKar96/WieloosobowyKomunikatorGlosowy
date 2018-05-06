using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WieloosobowyKomunikatorGlosowy
{
    class SHA
    {
        public static string ChangeToSHA2_256(string input)
        {
            using (SHA256Managed sha1 = new SHA256Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                string result = sb.ToString();
                return result.ToLower();
            }
        }
    }
}
