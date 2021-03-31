using System;

namespace ColumnarTranspositionCipher
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Type the text to encode: ");
            string clearText = Console.ReadLine();

            Console.Write("Type the key to use: ");
            string key = Console.ReadLine();


            try
            {
                Encipher encipher = new Encipher(clearText, key);
                encipher.PrintGrid(true);
                string encipheredText = encipher.DoCipher();
                Console.WriteLine(encipheredText);
                Decipher decipher = new Decipher(encipheredText, key);
                decipher.PrintGrid(true);
                Console.WriteLine(decipher.DoDecipher());
            } catch (ArgumentException e)
            {
                Console.Error.WriteLine($"Error: {e.Message}");
            }
        }
    }
}