using System;
using System.Linq;

namespace ColumnarTranspositionCipher
{
    public class Decipher : Cipher
    {
        public Decipher(string encryptedText, string key) : base(key)
        {
            if (encryptedText.Length % Key.Length > 0)
            {
                throw new ArgumentException($"The key '{Key}' has an unexpected length.");
            }

            int rows = encryptedText.Length / Key.Length;

            Grid = new char[rows, Key.Length];
            FillGrid(encryptedText);
        }

        private void FillGrid(string text)
        {
            int stringPosition = 0;
            string sortedKey = string.Concat(Key.OrderBy(c => c));
            foreach (char ch in sortedKey)
            {
                int column = Key.IndexOf(ch);
                for (int i = 0; i < Grid.GetLength(Row); i++)
                {
                    Grid[i, column] = GetChar(text.ToUpper(), stringPosition++);
                }
            }
        }

        public string DoDecipher()
        {
            string clearText = "";
            for (int i = 0; i < Grid.GetLength(Row); i++)
            {
                for (int j = 0; j < Grid.GetLength(Column); j++)
                {
                    clearText += Grid[i, j];
                }
            }

            return clearText;
        }
    }
}