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
        static List<string> lettersUsed = new List<string>();
        static List<string> charInWord = new List<string>();
            
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
            Creates an array the size of the word and fills it with blanks
        -----------------------*/
        public static string[] GetWord()
        {
            //Assign random word to variable
            var rwg = new Random();
            int randIndex = rwg.Next(wordList.Count());
            wordToGuess = wordList[randIndex];
            string[] toDisplay = new string[wordToGuess.Count()];

            Console.WriteLine(wordToGuess);

            for (int i = 0; i < wordToGuess.Count(); i++)
            {
                charInWord.Add(wordToGuess.Substring(i));
            }

            for (int i = 0; i < charInWord.Count(); i++)
            {
                toDisplay[i] = "_ ";
            }

            return toDisplay;

        }

        /*------------------------
        DisplayBoard()
            Accepts and validates the user's guess
        -----------------------*/
        public static void DisplayBoard(string[] toDisplay)
        {
            for (int i = 0; i < charInWord.Count(); i++)
            {
                Console.WriteLine(toDisplay[i]);
            }

            Console.WriteLine("");
        }


        /*------------------------
        GetGuess()
            Accepts and validates the user's guess
        -----------------------*/
        public static void GetGuess(string[] toDisplay)
        {
            Console.WriteLine("Please enter your guess: ");
            string guess = Console.ReadLine();
            int guessIndex = 0;

            for (int i = 0; i < charInWord.Count(); i += guessIndex)
            {
                if (charInWord[i] == guess)
                {
                    guessIndex = wordToGuess.IndexOf(guess);
                    Console.WriteLine(guessIndex);
                    toDisplay[guessIndex] = guess;
                    
                }
            }

            //string found = myList.Find(x => x.StartsWith("j"));

        }

    }
}
