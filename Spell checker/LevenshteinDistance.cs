using System;

namespace Spell_checker
{
    public static class LevenshteinDistance
    { 
        public static int GetDistance(string source, string target)
        {
            var distance = new int[source.Length, target.Length];
            distance[0, 0] = 0;
            var sourceLength = source.Length;
            var targetLength = target.Length;
            
            for (var j = 1; j < targetLength; j++)
            {
                distance[0, j] = distance[0, j - 1] + 1;
            }
            
            for (var i = 1; i < sourceLength; i++)
            {
                distance[i, 0] = distance[i - 1, 0] + 1;
                for (var j = 1; j < targetLength; j++)
                {
                    if (source[i] != target[j])
                    {
                        var min1 = distance[i - 1, j] + 1;
                        var min2 = distance[i, j - 1] + 1;
                        distance[i, j] = Math.Min(min1, min2);
                    }
                    else
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                }
            }

            return distance[sourceLength - 1, targetLength - 1];
        }
    }
}