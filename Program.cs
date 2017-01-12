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

        static string wordToGuess;
        static int guessCount = 0;
        static List<string> lettersUsed;
        static string[] charInWord;

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
            Creates a reandom index and writes the word that is at that index
            Creates an array the size of the word and fills it with blanks
        -----------------------*/
        public static void GetWord()
        {
            //Assign random word to variable
            var rwg = new Random();
            int randIndex = rwg.Next(wordList.Count());
            wordToGuess = wordList[randIndex];

            Console.WriteLine(wordToGuess);

            //Creates an array of blanks from the word
            charInWord = new string[wordToGuess.Count()];

            for (int i = 0; i < charInWord.Count(); i++)
            {
                charInWord[i] = "_ ";
            }

        }

        /*------------------------
        DisplayBoard()
            Accepts and validates the user's guess
        -----------------------*/
        public static void DisplayBoard()
        {
            for (int i = 0; i < charInWord.Count(); i++)
            {
                Console.WriteLine(charInWord[i]);
            }
        }


        /*------------------------
        GetGuess()
            Accepts and validates the user's guess
        -----------------------*/
        public static void GetGuess()
        {
            Console.WriteLine("Please enter your guess: ");
            string guess = Console.ReadLine();
            int guessIndex;
            bool checkMulti = true;

            //If letter has not already been guessed
            //lettersUsed.Add(guess);
            while (checkMulti)
            {
                if (wordToGuess.Contains(guess))
                {
                    guessIndex = wordToGuess.IndexOf(guess);
                    charInWord[guessIndex] = guess;
                }
                else
                {
                    Console.WriteLine("That letter isn't in the word!");
                    checkMulti = false;
                }
            }
        

        }

    }
}
