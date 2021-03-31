using System;
using System.Linq;

namespace Columnar_Transposition_Cipher
{
    public class Decipher : Cipher
    {
        public Decipher(string encryptedText, string key)
        {
            Key = key.ToUpper().Trim();
            if (!IsKeyValid())
            {
                throw new ArgumentException(
                    $"{Key} is not a valid key. The key must be at least 2 characters, only use alphanumeric " +
                    $"characters, and can't contain duplicate characters.");
            }

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