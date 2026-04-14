using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace P2
{
    class Program
    {
        // This function will help us get the input from the command line
        public static string GetInputFromCommandLine(string[] args)
        {
            // get the input from the command line
            string input = "";
            if (args.Length == 1)
            {
                input = args[0]; // Gets the first string after the 'dotnet run' command
            }
            else
            {
                Console.WriteLine("Not enough or too many inputs provided after 'dotnet run' ");
            }
            return input;
        }

        // compute md5 with salt
        public static string compute_md5_with_salt(string plaintext, string salt)
        {
            string res;
            byte[] data_before_salt = Encoding.UTF8.GetBytes(plaintext);
            byte[] data_after_salt = new byte[data_before_salt.Length + 1];
            for(int i = 0; i < data_before_salt.Length; i++)
            {
                data_after_salt[i] = data_before_salt[i];
            }
            data_after_salt[data_before_salt.Length] = Convert.ToByte(salt, 16);

            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(data_after_salt);
            res = BitConverter.ToString(data).Replace("-", " ").Substring(0, 14);
            return res;                        
        }
        
        // create random plaintext with same length
        public static string randPlaintext(string alphnum, int len)
        {
            string res = "";
            for(int i = 0; i < len; i++)
            {
                Random rand = new Random();
                int randnum = rand.Next(0, 36); 
                res += alphnum[randnum];               
            }
            return res;
        }
        
        public static string P2(string[] args)
        {
            string alphnum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string salt = GetInputFromCommandLine(args);
            
            string p1 = "";
            string p2 = "";
            int len = 10;
            bool flag = false;
            Dictionary<string, string> md5hashwithsalt_plaintext =
                new Dictionary<string, string>();
            
            while(!flag)
            {
                string new_plaintext = randPlaintext(alphnum, len);
                string new_md5 = compute_md5_with_salt(new_plaintext, salt);
                
                if(md5hashwithsalt_plaintext.ContainsKey(new_md5) == false)
                    md5hashwithsalt_plaintext.Add(new_md5, new_plaintext);
                else if(md5hashwithsalt_plaintext[new_md5] != new_plaintext)
                {
                    p1 = md5hashwithsalt_plaintext[new_md5];
                    p2 = new_plaintext;
                    break;
                }
            }

            string P2_answer = p1 + "," + p2;
            Console.WriteLine(P2_answer); 

            // return answer to the autograder
            return P2_answer; 
        }

        static void Main(string[] args)
        {
            // args is array with the command line inputs
            P2(args); 
        }

    }
}