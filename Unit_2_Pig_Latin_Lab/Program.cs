using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main()
    {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '/', '\\', '*' };


        Console.WriteLine("Welcome to the Pig Latin Translator!\n");

        while (true)
        {
            Console.Write("Enter a line to be translated: ");
            string sentence = Console.ReadLine();

            string[] words = sentence.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                bool containsVowel = false;
                bool containsNumOrSymbols = false;
                foreach (char e in words[i])
                {
                    if (nums.Contains(e))
                    {
                        containsNumOrSymbols = true;
                        break;
                    }
                }
                if (containsNumOrSymbols)
                    continue;

                if (vowels.Contains(words[i][0])) // Beginner letter is already a vowel
                {
                    words[i] += "way";
                    continue;
                }
                bool firstCase = false;
                while (!vowels.Contains(words[i][0])) // Beginning letter is consonant, move to end
                {
                    // Check if the word contains any vowel, if not, don't manipulate string
                    foreach (char c in words[i])
                    {
                        if (vowels.Contains(c))
                            containsVowel = true;
                    }
                    if (!containsVowel)
                        break;

                    // If the first letter of the word is uppercase, preserve uppercase in final string
                    if (char.IsUpper(words[i][0]))
                        firstCase = true;

                    // Move consonants to the end of the string until a vowel is at the front
                    words[i] += char.ToLower(words[i][0]);
                    words[i] = words[i].Remove(0, 1);
                }
                if (!containsVowel)
                    continue;

                words[i] += "ay";
                if (firstCase) // Making first letter uppercase
                {
                    words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
                }
            }
            foreach (var elem in words)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();

            Console.Write("Translate another line? y/n: ");
            if (Console.ReadLine() == "n")
                break;
        }
    }
}