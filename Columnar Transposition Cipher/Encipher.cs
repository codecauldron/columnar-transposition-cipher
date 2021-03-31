using System;
using System.Linq;

namespace ColumnarTranspositionCipher
{
    public class Encipher : Cipher
    {
        public Encipher(string clearText, string key) : base(key)
        {
            if (!IsKeyValid())
            {
                throw new ArgumentException(
                    $"{Key} is not a valid key. The key must be at least 2 characters, only use alphanumeric " +
                    $"characters, and can't contain duplicate characters.");
            }

            int rows = clearText.Length / Key.Length + (clearText.Length % Key.Length > 0 ? 1 : 0);

            Grid = new char[rows, Key.Length];
            FillGrid(clearText);
        }

        public string DoCipher()
        {
            string cipheredText = "";
            string sortedKey = string.Concat(Key.OrderBy(c => c));
            foreach (var ch in sortedKey)
            {
                int column = Key.IndexOf(ch);
                cipheredText += GetColumnText(column);
            }

            return cipheredText;
        }

        private void FillGrid(string text)
        {
            int stringPosition = 0;
            for (int i = 0; i < Grid.GetLength(Row); i++)
            {
                for (int j = 0; j < Grid.GetLength(Column); j++)
                {
                    Grid[i, j] = GetChar(text.ToUpper(), stringPosition++);
                }
            }
        }
    }
}