using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spell_checker
{
    public static class InputReader
    {
        private const string OutputContinuation = "\nPRESS ANY KEY TO CONTINUE;\n\"Esc\" TO EXIT;";
        private static readonly char[] Separators = {' ', '.', ',', ';', ':', '!', '?', '\n', '\t', '\r', '\"', '=', '(', ')', '@', '#', '%', '&', '*', '[', ']'};
        public static void Start()
        {
            var keyPressed = new ConsoleKeyInfo().Key;
            while (keyPressed != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("ENTER AN INPUT");
                var input = Console.ReadLine();
                CorrectInput(input);
                Console.WriteLine(OutputContinuation);
                keyPressed = Console.ReadKey().Key;
            }
        }

        public static (string dictionary, IEnumerable<string> sentences) Split(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException();
            var commonSplit = input.Split("===", StringSplitOptions.RemoveEmptyEntries);
            return (commonSplit.First(), commonSplit.Skip(1));
        }

        private static void CorrectInput(string input)
        {
            string dictionary;
            IEnumerable<string> sentences;
            try
            {
                (dictionary, sentences) = Split(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            var spellChecker = new SpellChecker(dictionary.Split(Separators, StringSplitOptions.RemoveEmptyEntries));
            foreach (var sentence in sentences)
            {
                if (string.IsNullOrWhiteSpace(sentence)) continue;
                Console.WriteLine($"SENTENCE BEFORE CORRECTIONS:\n{sentence}\n");
                var correctedSentence = CorrectSentence(spellChecker, sentence);
                Console.WriteLine("CORRECTED SENTENCE:\n" + correctedSentence);
                Console.WriteLine("=============================================");
            }
        }

        private static string CorrectSentence(SpellChecker spellChecker, string sentence)
        {
            var words = sentence.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string>();
            foreach (var word in words)
            {
                result.Add(spellChecker.GetCorrectForm(word));
            }

            return string.Join(" ", result);
        }
    }
}