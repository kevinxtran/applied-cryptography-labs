using System;
using System.IO;
using System.Security.Cryptography;

namespace P1_2 
{
    class Program 
    {
        private static string Encrypt(byte[] key, string secretString) 
        {
            DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, csp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(secretString);
            sw.Flush();
            cs.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int) ms.Length);
        }

        static void Main(string[] args) 
        {
            string plain_text = args[0], cipher_text = args[1]; 
            DateTime start_dt, end_dt;

            if (args.Length == 3) 
            {
                // sample expected input range "7/3/2020/11/00-7/4/2020/11/00". Note: resolution set to minutes
                string[] user_date_range = args[2].Split("-");
                int[] start_dt_int = Array.ConvertAll(user_date_range[0].Split("/"), s => int.Parse(s));
                int[] end_dt_int = Array.ConvertAll(user_date_range[1].Split("/"), s => int.Parse(s));
                start_dt = new DateTime(start_dt_int[2], start_dt_int[0], start_dt_int[1], start_dt_int[3], start_dt_int[4], 0);
                end_dt = new DateTime(end_dt_int[2], end_dt_int[0], end_dt_int[1], end_dt_int[3], end_dt_int[4], 0);
            } 
            else 
            {
                // default range
                start_dt = new DateTime(2020, 7, 3, 11, 0, 0);
                end_dt = new DateTime(2020, 7, 4, 11, 0, 0);
            }

            Console.WriteLine($"plain_text: {plain_text}\ncipher_text: {cipher_text}\ndate_range: {start_dt} - {end_dt}\n");

            // test each datetime obj (resolution down to the minute) as the seed until it matches the expected cipher_Text
            for (var dt = start_dt; dt.Date <= end_dt; dt = dt.AddMinutes(1)) 
            {
                TimeSpan ts = dt.Subtract(new DateTime(1970, 1, 1));
                int seed = (int) ts.TotalMinutes;
                Random rng = new Random(seed);
                byte[] key = BitConverter.GetBytes(rng.NextDouble());

                if (Encrypt(key, plain_text) == cipher_text)
                { 
                Console.WriteLine(ts.TotalMinutes);
                break;
                }
                seed++; 
            }
        }
    }
}