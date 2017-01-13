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
        static bool noWinner = true;
        static int guessCount = 8;
        static string testMode;

        /*------------------------
        Main()
        -----------------------*/
        static void Main(string[] args)
        {
            bool testModeValid = true;

            GetWord();

            //Check (and validate) to see if user wants to play in test mode
            while (testModeValid)
            {
                Console.WriteLine("Would you like to play in test mode? (Y/N): ");
                testMode = Console.ReadLine();

                if (testMode.ToLower() == "y" || testMode.ToLower() == "n")
                {
                    testModeValid = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                    Console.ReadLine();
                }
            }

            //While user still has guesses left and has not guessed the word...
            while (guessCount > 0 && noWinner)
            {
                Console.Clear();

                //If in test mode, show word
                if (testMode.ToLower() == "y")
                {
                    Console.WriteLine(word);
                }

                DisplayBoard();
                GetGuess();
                CheckForWinLoss();
            }

            if (noWinner == false)
            {
                Console.WriteLine($"Congratulations! The word was {word}!");
                DisplayBoard();
            }

            //If out of guesses...
            else if (guessCount == 0)
            {
                Console.Clear();
                blanks.ForEach(Console.Write);
                Console.WriteLine("You ran out of guesses. " +
                    $"The word was {word}.");
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

        }
        
        /*------------------------
        DisplayBoard()
            Displays number of guesses remaining, letters guessed, and blanks<>
        -----------------------*/
        public static void DisplayBoard()
        {
            //If not user's first turn
            if (charsUsed.Count() > 0)
            {
                Console.WriteLine($"You have {guessCount} guesses left! ");
                Console.Write("So far, you have guessed: ");
                charsUsed.ForEach(Console.Write);
                Console.WriteLine("\n");
            }

            //Display blanks
            blanks.ForEach(Console.Write);

        }

        /*------------------------
        GetGuess()
            Accepts the user's guess and calls for validation
        -----------------------*/
        public static void GetGuess()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Take a guess! ");
            string guess = Console.ReadLine();

            //If guess contains only one letter (no ints) then validate
            if (guess.All(char.IsLetter) && guess.Length == 1) {
                ValidateGuess(guess);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{guess} is invalid.");
                Console.ReadLine();
            }

        }

        /*------------------------
        ValidateGuess()
            Accepts and validates the user's guess
        -----------------------*/
        public static void ValidateGuess(string guess)
        {
            List<int> charIndexes = new List<int>();
            bool decrGuess = true; //Decrement Guess Counter

            //If character has already been guessed, try again 
            if (charsUsed.Contains(guess))
            {
                Console.WriteLine($"Sorry! \"{guess}\" has already been guessed! " +
                    "Press ENTER to try again.");
                Console.ReadLine();
                decrGuess = false;
            }
            else
            {
                //Add the character that was guessed to the list of characters used
                charsUsed.Add(guess);

                //If guess matches a letter in the word, catalog the index of the letter
                for (int i = 0; i < word.Length; i++)
                {
                    if (characters[i] == guess)
                    {
                        charIndexes.Add(i);
                        decrGuess = false;
                    }
                }

                //If guess matched, change the value at the index(es)cataloged
                for (int i = 0; i < charIndexes.Count(); i++)
                {
                    int x = charIndexes[i];
                    blanks[x] = characters[x];
                }
            }

            //If answer was incorrect, decrement the number of guesses left
            if (decrGuess)
            {
                guessCount--;
                Console.WriteLine($"Sorry, \"{guess}\" is not in the word! " +
                    "Press ENTER to try again.");
                Console.ReadLine();
                Console.Clear();
            }

            Console.Clear();

            //Clear indexes of matched letters
            charIndexes.Clear();

            //Reset decrement boolean
            decrGuess = false;

        }

        /*------------------------
        CheckForWinLoss()
            Checks to see if all of the spaces are filled correctly
        -----------------------*/
        public static void CheckForWinLoss()
        {
            //If all letters have been guessed...
            if (!blanks.Contains("_ "))
            {
                noWinner = false;
            }
            
        }

    }

}

