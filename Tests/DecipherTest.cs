using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace ColumnarTranspositionCipher
{
    public class DecipherTest
    {
        private const string RANDOM_FILL = "[A-Z]{3}";
        private const string CLEAR_TEXT_PATTERN = "THIS IS THE CLEAR TEXT" + RANDOM_FILL;
        private const string CIPHERED_TEXT = " HEEAI C BHS RTSTLTCTIEAX";
        
        private Decipher decipher;

        [Test]
        public void ItShouldCipher()
        {
            const string KEY = "zebra";

            decipher = new Decipher(CIPHERED_TEXT, KEY);
            string clearText = decipher.DoDecipher();
            Assert.AreEqual(true, Regex.IsMatch(clearText, CLEAR_TEXT_PATTERN));
        }

        [Test]
        public void TestTooShortKey()
        {
            const string KEY = "A";
            Assert.Throws<ArgumentException>(() => new Decipher(CIPHERED_TEXT, KEY));
        }

        [Test]
        public void TestDuplicateCharInKey()
        {
            string key = "ABCA";
            Assert.Throws<ArgumentException>(() => new Decipher(CIPHERED_TEXT, key));

            key = "BCDD";
            Assert.Throws<ArgumentException>(() => new Decipher(CIPHERED_TEXT, key));
        }

        [Test]
        public void TestInvalidCharInKey()
        {
            string key = "A#CD";
            Assert.Throws<ArgumentException>(() => new Decipher(CIPHERED_TEXT, key));

            key = "?!BD";
            Assert.Throws<ArgumentException>(() => new Decipher(CIPHERED_TEXT, key));
        }
    }
}