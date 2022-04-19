using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spell_checker
{
    public class SpellChecker : ICorrectnessChecker<string>
    {
        private readonly HashSet<string> _nonOrderedForms;
        private readonly Dictionary<int, List<string>> _correctForms;

        public SpellChecker()
        {
            _nonOrderedForms = new HashSet<string>();
            _correctForms = new Dictionary<int, List<string>>();
        }
        
        /// <param name="toAdd">Strings that supposed to be added to list of correct words</param>
        public SpellChecker(IEnumerable<string> toAdd) : this()
        {
            Add(toAdd);
        }

        /// <param name="toAdd">Strings that supposed to be added to list of correct words</param>
        public void Add(IEnumerable<string> toAdd)
        {
            foreach (var form in toAdd)
            {
                if (string.IsNullOrWhiteSpace(form)) throw new ArgumentException();
                
                var loweredSingleForm = form.ToLower(); 
                _nonOrderedForms.Add(loweredSingleForm);
                
                if (!_correctForms.ContainsKey(form.Length))
                    _correctForms[form.Length] = new List<string>();
                _correctForms[form.Length].Add(loweredSingleForm);
            }
        }

        /// <param name="source">Element to check</param>
        /// <returns>
        /// Correct form in case if it in list of correct elements;<br/>
        /// {W1 W2 ...} in case if were found several forms;<br/>
        /// {W?} in case if no corrections was found;
        /// </returns>
        public string GetCorrectForm(string source)
        {
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException();
            
            var possibleForms = GetPossibleFormsWithLevDistance(source).ToList();
            if (!possibleForms.Any()) return $"{{{source}?}}";
            var minDist = possibleForms.Min(form => form.levDist);
            var correctForms = possibleForms.Where(form => form.levDist == minDist).ToList();
            
            if (correctForms.Count == 1) return correctForms[0].form;
            return "{" + string.Join(" ", correctForms.Select(form => form.form)) + "}";
        }

        private IEnumerable<(string form, int levDist)> GetPossibleFormsWithLevDistance(string source)
        {
            var sourceLength = source.Length;
            var minLength = sourceLength < 3 ? 1 : sourceLength - 2;
            var maxLength = sourceLength + 2;
            var possibleForms = new List<(string, int)>();
            
            for (var i = minLength; i <= maxLength; i++)
            {
                if (_correctForms.TryGetValue(i, out var values))
                {
                    foreach (var value in values)
                    {
                        var levDistance = LevenshteinDistance.GetDistance(source, value);
                        if (levDistance <= 2)
                        {
                            if (levDistance == 2 && !IsCorrectionsValid(source, value)) continue; 
                            possibleForms.Add((value, levDistance));
                        }
                    }
                }
            }

            return possibleForms;
        }

        /// <summary>
        /// Is the edits are not both insertions or both deletions
        /// and made on adjacent characters
        /// </summary>
        private static bool IsCorrectionsValid(string source, string correct)
        {
            if (Math.Abs(source.Length - correct.Length) < 2) return true;

            var longerStr = source.Length > correct.Length ? source : correct;
            var shorterStr = longerStr == source ? correct : source;

            return shorterStr.Where((t, i) => longerStr[i] != t && t == longerStr[i + 1]).Any();
        }
    }
}