using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace ColumnarTranspositionCipher
{
    public class EncipherTest
    {
        private const string CLEAR_TEXT = "This is the clear text";
        private const string RANDOM_FILL = "[A-Z]";
        private Encipher encipher;

        [Test]
        public void ItShouldCipher()
        {
            const string KEY = "zebra";
            string pattern = $" HEE{RANDOM_FILL}I C {RANDOM_FILL}HS RTSTLT{RANDOM_FILL}TIEAX";

            encipher = new Encipher(CLEAR_TEXT, KEY);
            string cipheredText = encipher.DoCipher();
            Assert.AreEqual(true, Regex.IsMatch(cipheredText, pattern));
        }

        [Test]
        public void TestTooShortKey()
        {
            const string KEY = "A";
            Assert.Throws<ArgumentException>(() => new Encipher(CLEAR_TEXT, KEY));
        }

        [Test]
        public void TestDuplicateCharInKey()
        {
            string key = "ABCA";
            Assert.Throws<ArgumentException>(() => new Encipher(CLEAR_TEXT, key));

            key = "BCDD";
            Assert.Throws<ArgumentException>(() => new Encipher(CLEAR_TEXT, key));
        }

        [Test]
        public void TestInvalidCharInKey()
        {
            string key = "A#CD";
            Assert.Throws<ArgumentException>(() => new Encipher(CLEAR_TEXT, key));

            key = "?!BD";
            Assert.Throws<ArgumentException>(() => new Encipher(CLEAR_TEXT, key));
        }
    }
}