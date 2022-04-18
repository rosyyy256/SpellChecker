using System;

namespace Spell_checker
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 30; i++)
            {
                Console.WriteLine($"{i} - {i & 1}");
            }

            Console.ReadKey();
        }
    }
}