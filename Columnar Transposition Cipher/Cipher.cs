using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Columnar_Transposition_Cipher
{
    public abstract class Cipher
    {
        private const string VALID_KEY_CHARACTERS = "^[A-Z0-9]+$";
        internal const int Row = 0;
        internal const int Column = 1;
        private const int CHAR_A = 65;
        private const int CHAR_Z = 90;
        protected char[,] Grid { get; init; }
        protected string Key { get; init; }


        internal bool IsKeyValid()
        {
            return Key.Length > 1 &&
                   !(
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
            return stringPosition >= text.Length
                ? (char) new Random().Next(CHAR_A, CHAR_Z + 1)
                : text[stringPosition];
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