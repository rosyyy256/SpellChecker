using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spell_checker;

namespace SpellCheckerTests
{
    [TestFixture]
    public class SpellCheckerTests
    {
        private string dictionary = "rain spain plain plaint pain main mainly the in on fall falls his was";
        private List<string> splitDictionary;

        [SetUp]
        public void Setup()
        {
            splitDictionary = dictionary.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
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
        [TestCase("in", "in")]
        [TestCase("pain", "pain")]
        public void OnSingleWordsTest(string source, string expected)
        {
            var spellChecker = new SpellChecker();
            spellChecker.Add(splitDictionary);
            var actual = spellChecker.GetCorrectForm(source);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("his", "hints")]
        [TestCase("abc", "abcde")]
        [TestCase("example", "exple")]
        [TestCase("example", "examabple")]
        [TestCase("example", "exampleab")]
        public void DontReturnCorrectionsOnAdjacentCharacters(string dict, string source)
        {
            var spellChecker = new SpellChecker(dict.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            var expected = "{" + source + "?}";
            var actual = spellChecker.GetCorrectForm(source);
            Assert.AreEqual(expected, actual);
        }
    }
}