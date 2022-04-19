using System.Collections.Generic;

namespace Spell_checker
{
    public interface ICorrectnessChecker<T>
    {
        /// <summary>
        /// Adds a list of correct elements to checker 
        /// </summary>
        /// <param name="toAdd">List of correct elements</param>
        public void Add(IEnumerable<T> toAdd);
        
        /// <param name="source">Element to check</param>
        /// <returns>
        /// Correct form of the element
        /// </returns>
        public T GetCorrectForm(T source);
    }
}