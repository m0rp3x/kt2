using System.Diagnostics.Tracing;
using System.Reflection.Metadata;
using System.Threading.Channels;
using System.Transactions;

namespace ConsoleApp5;

using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string word = "huesos";
        string wordUnknown = new (word.Select(_ => '?').ToArray());
        int lives = 3;
        while (wordUnknown.Contains('?') & lives > 0)
        {
            Console.Clear();
            Console.WriteLine($"Lives: {lives}\n{wordUnknown}\nInput char > ");
            char input = Console.ReadLine().ToCharArray()[0];
                if (word.Contains(input))
                {
                    wordUnknown = new string(wordUnknown.Select((x, ind) => word[ind] == input ? input : x).ToArray());
                    continue;
                }

                lives -= 1;
        }
        Console.WriteLine($"{word}\n{(lives > 0 ? "You won" : "You lost")}");
    }
}