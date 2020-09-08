using System;
using System.IO;

namespace ShiftCipherBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentPath = System.IO.Path.GetFullPath(@"..\..\..\");
            string cipher = File.ReadAllText(Path.Combine(currentPath, "cipher.txt"));


            CipherDecrypter decrypter = new ShiftCipherDecrypter(Path.Combine(currentPath, "patterns.txt"));
            string shift = decrypter.Decrypt(cipher);

            Console.WriteLine("Ciphered Text: {0}\n", cipher);
            Console.WriteLine("Decrypted Text: {0}", shift);
            Console.ReadLine();
        }
    }
}
