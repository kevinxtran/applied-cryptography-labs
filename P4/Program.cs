using System;
using System.Numerics; 
using System.Collections.Generic;

namespace P4
{
    class Program
    {   
        public static BigInteger exeualg(BigInteger a, BigInteger b)
        {
            BigInteger x = 0, y = 1, u = 1, v = 0;
            BigInteger q = 0, r = 0, s = 0, t = 0;
            while(a != 0)
            {
                q = b / a;
                r = b % a;
                s = x - u * q;
                t = y - v * q;
                b = a;
                a = r;
                x = u;
                y = v;
                u = s;
                v = t;
            }
            return x;
        } 
        
        public static string P4(string[] args)
        {
            //given e value
            BigInteger e = 65537;
            int p_e = 0, p_c = 0, q_e = 0, q_c = 0;
            BigInteger cipher = 0;
            BigInteger plain = 0;

            //index
            int index = 1;
            foreach(var item in args)
            {   
                //get the input
                switch (index)
                {
                    case 1:
                        p_e = int.Parse(item);
                        break;
                    case 2:
                        p_c = int.Parse(item);
                        break;
                    case 3:
                        q_e = int.Parse(item);
                        break;
                    case 4:
                        q_c = int.Parse(item);
                        break;
                    case 5:
                        cipher = BigInteger.Parse(item);
                        break;
                    case 6:
                        plain = BigInteger.Parse(item);
                        break;
                    default:
                        break;
                }
                index++;
            }
            
            BigInteger p = 0, q = 0;
            p = BigInteger.Subtract(BigInteger.Pow(2, p_e), p_c);
            q = BigInteger.Subtract(BigInteger.Pow(2, q_e), q_c);
            BigInteger phi_n = BigInteger.Multiply(p - 1, q - 1);
            
            BigInteger d = exeualg(e, phi_n);

            //decryption
            BigInteger r1 = BigInteger.ModPow(cipher, d, p * q);

            //encryption
            BigInteger r2 = BigInteger.ModPow(plain, e, p * q);
            
            // dotnet run 254 1223 251 1339 66536047120374145538916787981868004206438539248910734713495276883724693574434582104900978079701174539167102706725422582788481727619546235440508214694579  1756026041
            
            string P4_ans = r1.ToString() + "," + r2.ToString();
            Console.WriteLine(P4_ans);
            return P4_ans;
        }

        static void Main(string[] args)
        {
            P4(args); 
        }
    }
}
