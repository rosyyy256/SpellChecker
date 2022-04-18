using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spell_checker;

namespace SpellCheckerTests
{
    public class SpellCheckerTests
    {
        private string dictionary = "rain spain plain plaint pain main mainly the in on fall falls his was";
        private string input = "hte rame in pain fells mainy oon teh lain was hints pliant";
        private List<string> splitDictionary;
        private List<string> splitInput;
        
        [SetUp]
        public void Setup()
        {
            splitDictionary = dictionary.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            splitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        

        [TestCase("hte", "the", 2)]
        [TestCase("teh", "the", 2)]
        [TestCase("mainy", "main", 1)]
        [TestCase("mainy", "mainly", 1)]
        [TestCase("fells", "falls", 2)]
        [TestCase("hte", "his", 4)]
        public void LevenshteinDistanceIsCorrectTest(string source, string target, int expected)
        {
            var actual = LevenshteinDistance.GetDistance(source, target);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("hte", "the")]
        [TestCase("rame", "{rame?}")]
        [TestCase("fells", "falls")]
        [TestCase("mainy", "{main mainly}")]
        [TestCase("oon", "on")]
        [TestCase("teh", "the")]
        [TestCase("lain", "plain")]
        [TestCase("hints", "{hints?}")]
        [TestCase("pliant", "plaint")]
        public void SpellCheckerOnSingleWordsTest(string source, string expected)
        {
            var spellChecker = new SpellChecker(splitDictionary);
            var actual = spellChecker.GetCorrectForm(source);
            Assert.AreEqual(expected, actual);
        }
    }
}