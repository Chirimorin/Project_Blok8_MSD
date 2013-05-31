using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Models
{
    public abstract class MD5Encryptor
    {

        
        //convert een string tot een md5 formatie en returnt deze
        public static string ConvertString(string input)
        {
            StringBuilder builder = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
            }
            return builder.ToString();
        }
        //voer een string en md5hash als input in om ze met elkaar te vergelijken (voor inloggen)
        public static bool CompareString(string input, string hash)
        {
            using (MD5 md5 = MD5.Create())
            {
                string inputhash = ConvertString(input);

                StringComparer compare = StringComparer.OrdinalIgnoreCase;

                if (0 == compare.Compare(inputhash, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
