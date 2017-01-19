using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuessingGame
{
    class Program
    {
        //
        // Defining constant global variables
        //

        private const int MAX_NUMBER_OF_PLAYER_GUESSES = 4;
        private const int MAX_NUMBER_TO_GUESS = 10;

        //
        // Defining other global variables
        //

        private static int playersGuess;
        private static int numberToGuess;
        private static int roundNumber;
        private static int numberOfWins;
        private static int numberOfCurrentPlayerGuess;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];
        private static bool playingGame;
        private static bool playingRound;
        private static bool numberGuessedCorrectly;
        private static Random random = new Random();

        static void Main(string[] args)
        {
            playingGame = DisplayWelcomeScreen();

            while (playingGame)
            {
                DisplayRulesScreen();

                //InitializeGame();

                //InitializeRound();
                QueryUserPlayAgain();
            }


            DisplayClosingScreen();

        }

        public static bool DisplayWelcomeScreen()
        {
            Console.WriteLine("Welcome to the Number Guessing Game!");
            Console.WriteLine();
            Console.WriteLine("Would you like to play? Type yes or no");
            Console.WriteLine();
            string userResponse = Console.ReadLine().ToUpper();

            if (userResponse == "YES")
            {
                playingGame = true;
            }

            return playingGame;
        }

        public static void DisplayRulesScreen()
        {
            Console.WriteLine("The computer will choose a secret number between 1 and 10.");
            Console.WriteLine("The user will guess one integer at a time, up to four times");
            Console.WriteLine("The computer will display if the guess is too low, too high");
            Console.WriteLine("or correct.");

            Console.WriteLine("Press any key to continue.");

            Console.ReadKey();

            //InitializeGame();

        }

        public static void InitializeGame()
        {
            DisplayRulesScreen();

            while (playingGame)
            {

                InitializeRound();

            }
        }

        public static void InitializeRound()
        {
            roundNumber = 1;

            numberToGuess = GetNumberToGuess();

        }

        public static int GetNumberToGuess()
        {
            return random.Next(0, 11);
        }
        /*
        public static int DisplayGetPlayerGuessScreen()
        {
            Console.WriteLine("Enter the number of your guess as an integer: ");
            int.TryParse(Console.ReadLine(), out playersGuess);
            
        }

        public static void DisplayPlayerGuessFeedback()
        {

        }

        public static int UpdateAndDisplayRoundStatus()
        {

        }

        public static void DisplayPlayerStats()
        {

        }
        */

        public static void DisplayClosingScreen()
        {
            Console.WriteLine("GET OUT OF HERE!");

            Console.WriteLine("Press any key to continue.");

            Console.ReadKey();
        }

        public static void DisplayNumbersGuessed()
        {

        }
        public static void DisplayReset()
        {

        }

        public static void DisplayContinueQuitPrompt()
        {

        }

        public static void QueryUserPlayAgain()
        {
            Console.WriteLine("Would you like to play again? Type yes or no");
            Console.WriteLine();
            string userResponse = Console.ReadLine().ToUpper();

            if (userResponse == "YES")
            {
                playingGame = true;
            }
            else
            {
                playingGame = false;
            }
        }
    }
}
