using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ColumnarTranspositionCipher
{
    public abstract class Cipher
    {
        private const string VALID_KEY_CHARACTERS = "^[A-Z0-9]{2,}$";
        private const int CHAR_A = 65;
        private const int CHAR_Z = 90;
        protected const int Row = 0;
        protected const int Column = 1;

        protected char[,] Grid { get; init; }
        protected string Key { get; }

        protected Cipher(string key)
        {
            Key = key.Trim().ToUpper();
            if (!IsKeyValid())
            {
                throw new ArgumentException(
                    $"{Key} is not a valid key. The key must be at least 2 characters, only use alphanumeric " +
                    "characters, and can't contain duplicate characters.");
            }
        }

        private bool IsKeyValid()
        {
            return !(
                       from c in Key
                       group c by c
                       into grp
                       where grp.Count() > 1
                       select grp.Key
                   ).Any() &&
                   Regex.IsMatch(Key, VALID_KEY_CHARACTERS);
        }

        internal char GetChar(string text, int stringPosition)
        {
            if (stringPosition >= text.Length)
            {
                return (char) new Random().Next(CHAR_A, CHAR_Z + 1);
            }

            return text[stringPosition];
        }

        internal string GetColumnText(int column)
        {
            string columnText = "";
            for (int i = 0; i < Grid.GetLength(Row); i++)
            {
                columnText += Grid[i, column];
            }

            return columnText;
        }

        public void PrintGrid(bool printHeader = false)
        {
            if (printHeader)
            {
                PrintHeader();
            }

            PrintLine();
            for (int i = 0; i < Grid.GetLength(Row); i++)
            {
                Console.Write("|");
                for (int j = 0; j < Grid.GetLength(Column); j++)
                {
                    Console.Write($"{Grid[i, j]}|");
                }

                Console.WriteLine();
            }

            PrintLine();
        }

        private void PrintHeader()
        {
            PrintLine();
            Console.Write("|");
            foreach (char ch in Key)
            {
                Console.Write($"{ch}|");
            }

            Console.WriteLine();
        }

        private void PrintLine()
        {
            Console.WriteLine("+".PadRight(Key.Length * 2, '-') + "+");
        }
    }
}