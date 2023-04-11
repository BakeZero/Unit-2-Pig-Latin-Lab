using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main()
    {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        Console.WriteLine("Welcome to the Pig Latin Translator!\n");

        while (true)
        {
            Console.Write("Enter a line to be translated: ");
            string sentence = Console.ReadLine();
            // Detect if an input is given before translation
            if (String.IsNullOrEmpty(sentence))
            {
                Console.WriteLine("No input detected, please try again");
                continue;
            }
            string[] words = sentence.Trim().Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                bool firstCase = (char.IsUpper(words[i][0])); // If the first letter of the word is uppercase, preserve uppercase in final string
                bool allCaps = Checker.CaseChecker(words[i]); // If all letters are all caps, make final string all caps
                words[i] = words[i].ToLower(); // Convert to lowercase before tranlsation

                // Don't manipulate words that contain numbers or special characters
                if (Checker.NumberChecker(words[i]))
                    continue;

                // If first letter is a vowel, simply add "way"
                if (vowels.Contains(words[i][0]))
                {
                    words[i] += "way";
                    continue;
                }

                if (Checker.VowelChecker(words[i], vowels)) // Manipulate only if the word contains a vowel
                {
                    // Beginning letter is consonant, move to end
                    while (!vowels.Contains(words[i][0]))
                    {
                        // Move consonants to the end of the string until a vowel is at the front
                        words[i] += char.ToLower(words[i][0]);
                        words[i] = words[i].Remove(0, 1);
                    }
                    words[i] += "ay";
                    if (allCaps) // Making word all caps
                    {
                        words[i] = words[i].ToUpper();
                    }
                    else if (firstCase) // Making first letter uppercase
                    {
                        words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
                    }
                    
                }
            }
            // Output modified string back to the user
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


public struct Checker
{
    /* Returns true if contains a vowel */
    public static bool VowelChecker(string word, char[] vowels)
    {
        foreach (char c in word)
        {
            if (vowels.Contains(c))
                return true;
        }
        return false;
    }

    /* Returns true if contains a number or special character */
    public static bool NumberChecker(string word)
    {
        char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '/', '\\', '*' };
        foreach (char e in word)
        {
            if (nums.Contains(e))
                return true;
        }
        return false;
    }

    /* Returns true if word is all caps */
    public static bool CaseChecker(string word)
    {
        foreach (char e in word)
        {
            if (Char.IsLower(e))
                return false;
        }
        return true;
    }
}