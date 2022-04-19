using System;
using System.Text;

namespace Spell_checker
{
    public static class LevenshteinDistance
    { 
        public static int GetDistance(string source, string target)
        {
            var sourceLength = source.Length;
            var targetLength = target.Length;
            if (source == null || target == null) throw new ArgumentNullException();
            var distance = new int[sourceLength + 1, targetLength + 1];

            for (var i = 0; i <= sourceLength; i++) distance[i, 0] = i;
            for (var j = 0; j <= targetLength; j++) distance[0, j] = j;

            for (var i = 1; i <= sourceLength; i++)
            {
                for (var j = 1; j <= targetLength; j++)
                {
                    var diff = source[i - 1] == target[j - 1] ? 0 : 1 + 1; //increase the cost of replacing a symbol so that it becomes equal to the price of one deletion and one insertion

                    distance[i, j] = Math.Min
                        (
                            Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), 
                            distance[i - 1, j - 1] + diff
                        );
                }
            }
            return distance[sourceLength, targetLength];
        }
    }
}