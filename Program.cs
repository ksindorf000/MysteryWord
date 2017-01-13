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
        static List<string> charsUsed = new List<string>();
        static bool winner = false;
        static int guessCount = 8;

        /*------------------------
        Main()
        -----------------------*/
        static void Main(string[] args)
        {
            GetWord();

            while (guessCount > 0)
            {
                DisplayBoard();
                CheckForWin();
                GetGuess();
                Console.WriteLine("");
            }

            if (guessCount == 0)
            {
                Console.WriteLine("You ran out of guesses. Sorry!");
                Console.ReadLine();
            }

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

            Console.WriteLine(word);

        }

        /*------------------------
        DisplayBoard()
            Accepts and validates the user's guess
        -----------------------*/
        public static void DisplayBoard()
        {
            for (int i = 0; i < word.Length; i++)
            {
                Console.Write(blanks.ElementAt(i));
            }

            Console.WriteLine($"\n You have {guessCount} guesses left! \n");
        }

        /*------------------------
        GetGuess()
            Accepts the user's guess and calls for validation
        -----------------------*/
        public static void GetGuess()
        {
            Console.WriteLine("Take a guess! ");
            string guess = Console.ReadLine();

            Validate(guess);
        }

        /*------------------------
        Validate()
            Accepts and validates the user's guess
        -----------------------*/
        public static void Validate(string guess)
        {
            List<int> charIndexes = new List<int>();
            bool decrGuess = true; //Derement Guess Counter

            //If character has already been guessed, try again until valid 
            if (charsUsed.Contains(guess))
            {
                GetGuess();
            }

            //If guess matches a letter in the word, catalog the index of the letter
            for (int i = 0; i < word.Length; i++)
            {
                if (characters[i] == guess)
                {
                    charIndexes.Add(i);
                    decrGuess = false;
                }
            }

            //If guess matched, change the value at the index(es) cataloged
            for (int i = 0; i < charIndexes.Count(); i ++)
            {
                int x = charIndexes[i];
                blanks[x] = characters[x];
            }

            //If answer was incorrect, decrement the number of guesses left
            if (decrGuess)
            {
                guessCount--;
                Console.WriteLine("Sorry, that letter is not in the word!");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Clear();
            }
            
            //Add the character that was guessed to the list of characters used
            charsUsed.Add(guess);

            charIndexes.Clear();

        }

        public static void CheckForWin()
        {
            for (int i = 0; i < word.Length; i ++)
            {
                winner = blanks[i] == characters[i] ? true : false;
            }
        }

    }

}

