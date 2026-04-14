using System;
using System.IO;
using System.Collections;

namespace P1_1
{
    class Program
    {
        public static byte[] Solve(byte[] inputBytes, byte[] bmpBytes)
        {      
            // bitwise XOR function 0xFF ^ 0xAB
            BitArray inputbits = new BitArray(inputBytes);

            byte[] resultByteArray = new byte[bmpBytes.Length]; // just a placeholder so that the code works from scatch without errors
            //initializing a bitmap index variable
            
            //main code here (below)
            for (int bmpIndex = 0; bmpIndex < 26; bmpIndex++) //take the first 26 bytes and keep originally
            {
                resultByteArray[bmpIndex] = bmpBytes[bmpIndex];
            }

            for (int bmpIndex = 26; bmpIndex < bmpBytes.Length; bmpIndex++) //take the bytes after 26 
            {
                //get bits to xor
                int indexOffset = bmpIndex - 26;
                //get left bit index
                //formula for little endian machines
                int leftBitIndex = (indexOffset/4)*8+(7-2*(indexOffset%4));
                int rightBitIndex = (leftBitIndex-1);
                byte xorByte = (byte)((Convert.ToByte(inputbits[leftBitIndex])<<1) | Convert.ToByte(inputbits[rightBitIndex]));
                //xor and store the bytes in result

                resultByteArray[bmpIndex] = (byte)(bmpBytes[bmpIndex]^xorByte);

            }

            return resultByteArray;
        }

        // function will help us get the input from command line
        public static string getInputFromCommandLine(string[] args)
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

        // Main function will run program
        static void Main(string[] args)
        {
            byte[] bmpBytes = new byte[]
            {
                0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00,
                0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00,
                0x18,0x00,0x00,0x00,0xFF,0xFF,0xFF,0xFF,
                0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,
                0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00,
                0x00,0x00,0xFF,0x00,0x00,0xFF,0xFF,0xFF,
                0xFF,0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,
                0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00,
                0x00,0x00
            };

            // get the input from the command line
            string input = getInputFromCommandLine(args);

            //split the string wrt space
            string[] strBytes = input.Split(' ');

            byte[] inputBytes = new byte[strBytes.Length]; 
            for (int i=0; i<strBytes.Length; i++)
            {
                inputBytes[i]=Convert.ToByte(strBytes[i],16);
            }
            
            byte[] solution = Solve(inputBytes, bmpBytes); 

            // Print the solution for the autograder in correct format
            Console.WriteLine(BitConverter.ToString(solution).Replace("-", " ")); 
        }

    }
}