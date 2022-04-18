using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spell_checker
{
    public class SpellChecker : ICorrectnessChecker<string>
    {
        private List<string> _orderedCorrectForms;
        private readonly HashSet<string> _nonOrderedForms;
        private readonly Dictionary<int, List<string>> _correctForms;

        public SpellChecker()
        {
            _orderedCorrectForms = new List<string>();
            _nonOrderedForms = new HashSet<string>();
            _correctForms = new Dictionary<int, List<string>>();
        }
        
        /// <param name="toAdd">Strings that supposed to be added to list of correct words</param>
        public SpellChecker(IEnumerable<string> toAdd) : this()
        {
            Add(toAdd);
        }

        public void Add(IEnumerable<string> toAdd)
        {
            _orderedCorrectForms.AddRange(toAdd.Select(str => str.ToLower()));
            foreach (var singleForm in toAdd)
            {
                _nonOrderedForms.Add(singleForm.ToLower());
                if (!_correctForms.ContainsKey(singleForm.Length))
                    _correctForms[singleForm.Length] = new List<string>();
                _correctForms[singleForm.Length].Add(singleForm.ToLower());
            }
            
            OrderCorrectForms();
        }

        public string GetCorrectForm(string source)
        {
            var correctForms = GetPossibleForms(source)
                .Where(form => LevenshteinDistance.GetDistance(source, form) <= 2);

            var correctFormsCount = correctForms.Count();
            if (correctFormsCount == 0) return $"{{{source}}}?";
            if (correctFormsCount == 1) return correctForms.First();
            var sb = new StringBuilder(string.Concat(correctForms, ' '));
            sb.Append('{');
            sb.Append(string.Concat(correctForms, ' '));
            sb.Append('}');
            return sb.ToString();
        }

        private List<string> GetPossibleForms(string source)
        {
            var sourceLength = source.Length;
            var minLength = sourceLength < 3 ? 1 : sourceLength - 2;
            var maxLength = sourceLength + 2;
            var possibleForms = new List<string>();
            
            for (var i = minLength; i <= maxLength; i++)
            {
                var values = new List<string>();
                if (_correctForms.TryGetValue(i, out values))
                {
                    possibleForms.AddRange(values);
                }
            }

            return possibleForms;
        }

        private void OrderCorrectForms()
        {
            _orderedCorrectForms = _orderedCorrectForms
                .OrderBy(element => element.Length)
                .ThenBy(element => element)
                .ToList();
        }
    }
}