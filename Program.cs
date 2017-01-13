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

        static int diffLevel = 0; //0=Easy, 1=Medium, 2=Hard, 3=All
        static List<string> easyList = new List<string>();
        static List<string> medList = new List<string>();
        static List<string> hardList = new List<string>();

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
            bool playAgain = true;

            while (playAgain)
            {
                playAgain = PlayGame();
            }

        }

        public static bool PlayGame()
        {
            bool playAgain = true;

            SelectMode();

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
                Console.WriteLine($"Congratulations! YOU WIN! \n" +
                    $"The word was {word}!" +
                    "Play again? (Y/N)");
                playAgain = Console.ReadLine().ToLower() == "y" ? true : false;
                noWinner = true;
            }

            //If out of guesses...
            else if (guessCount == 0)
            {
                Console.Clear();
                blanks.ForEach(Console.Write);
                Console.WriteLine("\n" + "You ran out of guesses. \n" +
                    $"The word was {word}." + "Play again? (Y/N)");
                playAgain = Console.ReadLine().ToLower() == "y" ? true : false;
                guessCount = 0;
            }

            Console.Clear();

            charsUsed.Clear();
            characters.Clear();
            blanks.Clear();
            word = string.Empty;

            return (playAgain);

        }

        /*------------------------
        SelectModes()
            Asks user for Test Mode
            Asks user for Difficulty Level
        -----------------------*/

        public static void SelectMode()
        {

            //Check (and validate) to get difficulty level
            string userChoice;
            bool validInput = true;

            while (validInput)
            {
                Console.WriteLine("Please choose a level of difficulty: \n" +
                    "Easy: words with up to 6 characters \n" +
                    "Medium: words with 6-10 characters \n" +
                    "Hard: words with 10+ characters \n" +
                    "Whatevs: words of all levels");
                userChoice = Console.ReadLine();

                if (userChoice.ToLower() != "easy" && userChoice.ToLower() != "medium" &&
                    userChoice.ToLower() != "hard" && userChoice.ToLower() != "whatevs")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                    Console.ReadLine();
                }
                else if (userChoice.ToLower() == "easy")
                {
                    diffLevel = 0;
                    validInput = false;
                }
                else if (userChoice.ToLower() == "medium")
                {
                    diffLevel = 1;
                    validInput = false;
                }
                else if (userChoice.ToLower() == "hard")
                {
                    diffLevel = 2;
                    validInput = false;
                }
                else if (userChoice.ToLower() == "whatevs")
                {
                    diffLevel = 3;
                    validInput = false;
                }

            }

            Console.Clear();

            //Check (and validate) to see if user wants to play in test mode
            bool testModeValid = true;

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

            GetWord();
        }

        /*------------------------
        GetWord()
            Creates a random index and gets the word that is at that list index
            Creates a List<string> to hold the characters of the word
            Creates a List<string> the size of the word and fills it with blanks
        -----------------------*/
        public static void GetWord()
        {
            CreateWordLists();

            int i;
            var rwg = new Random();
            int randIndex;

            //EASY//
            if (diffLevel == 0)
            {
                randIndex = rwg.Next(easyList.Count());
                word = easyList[randIndex];
            }
            //MEDIUM//
            else if (diffLevel == 1)
            {
                randIndex = rwg.Next(medList.Count());
                word = medList[randIndex];
            }
            //HARD//
            else if (diffLevel == 2)
            {
                randIndex = rwg.Next(hardList.Count());
                word = hardList[randIndex];
            }
            //ALL//
            else if (diffLevel == 4)
            {
                randIndex = rwg.Next(wordList.Count());
                word = wordList[randIndex];
            }

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
        CreateWordLists()
            Creates a different word list for each level of difficulty
        -----------------------*/
        public static void CreateWordLists()
        {
            //Easy Mode List (up to 6 characters)
            if (diffLevel == 0)
            {
                for (int i = 0; i < wordList.Length; i++)
                {
                    if (wordList[i].Length <= 6)
                    {
                        easyList.Add(wordList[i]);
                    }
                }
            }
            else if (diffLevel == 1)
            {
                //Medium Mode List (6-10 characters)
                for (int i = 0; i < wordList.Length; i++)
                {
                    if (wordList[i].Length >= 6 && wordList[i].Length <= 10)
                    {
                        medList.Add(wordList[i]);
                    }
                }
            }
            else if (diffLevel == 2)
            {
                //Hard Mode List (10+ characters)
                for (int i = 0; i < wordList.Length; i++)
                {
                    if (wordList[i].Length >= 10)
                    {
                        hardList.Add(wordList[i]);
                    }
                }


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
            if (guess.All(char.IsLetter) && guess.Length == 1)
            {
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

