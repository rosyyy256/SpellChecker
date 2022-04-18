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
        
        /// <param name="toCheck">Element form to check</param>
        /// <returns>
        /// Correct form in case if it in list of correct elements;<br/>
        /// {W1 W2 ...} in case if were found several forms;<br/>
        /// {W?} in case if no corrections was found;
        /// </returns>
        public T GetCorrectForm(T toCheck);
    }
}