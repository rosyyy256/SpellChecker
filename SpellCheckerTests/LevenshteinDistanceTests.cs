using NUnit.Framework;
using Spell_checker;

namespace SpellCheckerTests
{
    [TestFixture]
    public class LevenshteinDistanceTests
    {
        [TestCase("plain", "plain", 0)]
        [TestCase("hte", "the", 2)]
        [TestCase("teh", "the", 2)]
        [TestCase("mainy", "main", 1)]
        [TestCase("mainy", "mainly", 1)]
        [TestCase("oon", "on", 1)]
        public void LevenshteinDistanceIsCorrectTest(string source, string target, int expected)
        {
            var actual = LevenshteinDistance.GetDistance(source, target);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("oon", "in", 3)]
        [TestCase("hte", "his", 4)]
        [TestCase("fells", "falls", 2)]
        public void LevenshteinDistanceDontReplaceSymbols(string source, string target, int expected)
        {
            var actual = LevenshteinDistance.GetDistance(source, target);
            Assert.AreEqual(expected, actual);
        }
    }
}