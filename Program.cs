using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryWord
{
    class Program
    {
        //Write contents of WordList into an Array
        static string[] wordList = File.ReadAllLines(@"..\..\WordList.txt");

        static string word;
        static List<string> characters = new List<string>();
        static List<string> blanks = new List<string>();


        /*------------------------
        Main()
        -----------------------*/
        static void Main(string[] args)
        {

            //while (guessCount <= 8)
            //{
            GetWord();
            DisplayBoard();
            GetGuess();
            DisplayBoard();


            //}
        }

        /*------------------------
        GetWord()
            Creates a random index and gets the word that is at that index
            Creates a List<string> to hold the characters of the word
            Creates a List<string> the size of the word and fills it with blanks
        -----------------------*/
        public static void GetWord()
        {
            //Assign random word to variable
            var rwg = new Random();
            int randIndex = rwg.Next(wordList.Count());
            word = wordList[randIndex];
            int i;

            //Add each character to the characters List
            for (i = 0; i < word.Length; i++)
            {
                characters.Add(word.Substring(i, 1));
            }

            //Add a blank for each character to the blanks list
            for (i = 0; i < word.Length; i++)
            {
                blanks.Add("_ ");
            }

        }

        /*------------------------
        DisplayBoard()
            Accepts and validates the user's guess
        -----------------------*/
        public static void DisplayBoard()
        {
            Console.WriteLine(word);

            for (int i = 0; i < word.Length; i++)
            {
                Console.Write(blanks.ElementAt(i));
            }

            Console.WriteLine();
        }


        /*------------------------
        GetGuess()
            Accepts and validates the user's guess
        -----------------------*/
        public static void GetGuess()
        {
            Console.WriteLine("Take a guess! ");
            string guess = Console.ReadLine();
            bool keepChecking = true;

            while (keepChecking)
            {
                if (characters.Contains(guess))
                {
                    keepChecking = false;
                }
            }

            if (!keepChecking)
            {
                int i = characters.IndexOf(guess);
                blanks[i] = characters[i];
            }

        }

    }

}

