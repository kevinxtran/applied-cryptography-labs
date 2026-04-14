using System;
using System.Security.Cryptography; 
using System.Numerics; 
using System.IO;

namespace P3 {
  class Program {
    public static string P3(string[] args) 
    {
      string iv128 = args[0];
      string ge = args[1];
      string gc = args[2];
      int ne = Int32.Parse(args[3]);
      BigInteger nc = BigInteger.Parse(args[4]);
      BigInteger x = BigInteger.Parse(args[5]);
      BigInteger gy = BigInteger.Parse(args[6]);
      string encrypted_message = args[7];
      string plaintext_message = args[8];

      BigInteger b2 = 2;
      BigInteger semiMod = BigInteger.Pow(b2, ne);
      BigInteger Mod = BigInteger.Subtract(semiMod, nc);

      BigInteger key = BigInteger.ModPow(gy, x, Mod);

      byte[] iv_byte = getBytesFromStrg(iv128);
      byte[] key_byte = key.ToByteArray();
      byte[] encrypted_message_byte = getBytesFromStrg(encrypted_message);

      using(Aes myAes = Aes.Create()) 
      {
        byte[] encrypted = EncryptStringToBytes_Aes(plaintext_message, key_byte, iv_byte);
        string decrypted = DecryptStringFromBytes_Aes(encrypted_message_byte, key_byte, iv_byte);
        string res1 = BitConverter.ToString(encrypted).Replace('-', ' ');
        string result = decrypted + "," + res1;
        Console.WriteLine(result);
        return result;
      }

      static byte[] getBytesFromStrg(string input) 
      {
        var input_split = input.Split(' ');
        byte[] inputBytes = new byte[input_split.Length];
        int i = 0;
        foreach(string item in input_split) {
          inputBytes.SetValue(Convert.ToByte(item, 16), i);
          i++;
        }
        return inputBytes;
      }

      static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV) 
      {
        if (plainText == null || plainText.Length <= 0)
          throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
          throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
          throw new ArgumentNullException("IV");
        byte[] encrypted;
        
        using(Aes aesAlg = Aes.Create()) {
          aesAlg.Key = Key;
          aesAlg.IV = IV;

          
          ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
          
          using(MemoryStream msEncrypt = new MemoryStream()) {
            using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) 
            {
              using(StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                swEncrypt.Write(plainText);
              }
              encrypted = msEncrypt.ToArray();
            }
          }
        }

        return encrypted;
      }

      static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key,
        byte[] IV) {
        if (cipherText == null || cipherText.Length <= 0)
          throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
          throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
          throw new ArgumentNullException("IV");
        // string to hold decrypted text

        string plaintext = null;
        // AES obj with the specified key and IV

        using(Aes aesAlg = Aes.Create()) {
          aesAlg.Key = Key;
          aesAlg.IV = IV;

          // decryptor for the stream transform
          ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
          using(MemoryStream msDecrypt = new MemoryStream(cipherText)) {
            using(CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) 
            {
              using(StreamReader srDecrypt = new StreamReader(csDecrypt)) 
              {
                plaintext = srDecrypt.ReadToEnd();
              }
            }
          }
        }
        return plaintext;
      }
    }
    
    static void Main(string[] args) 
    {
      P3(args);
    }
  }
}

/* run code with line
dotnet run "A2 2D 93 61 7F DC 0D 8E C6 3E A7 74 51 1B 24 B2" 251 465 255 1311 2101864342 8995936589171851885163650660432521853327227178155593274584417851704581358902 "F2 2C 95 FC 6B 98 BE 40 AE AD 9C 07 20 3B B3 9F F8 2F 6D 2D 69 D6 5D 40 0A 75 45 80 45 F2 DE C8 6E C0 FF 33 A4 97 8A AF 4A CD 6E 50 86 AA 3E DF" AfYw7Z6RzU9ZaGUloPhH3QpfA1AXWxnCGAXAwk3f6MoTx
*/