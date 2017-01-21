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
        private static int numberOfLosses;
        private static int numberOfCurrentPlayerGuess;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];
        private static bool playingGame;
        private static bool playingRound;
        private static bool numberGuessedCorrectly;
        private static Random random = new Random();

        static void Main(string[] args)
        {
            //
            // Display welcome screen, query user to play or not, initialize game variables.
            //
            playingGame = DisplayWelcomeScreen();
            InitializeGame();
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
        }

        public static void InitializeGame()
        {
            
            DisplayReset();
            roundNumber = 0;
            DisplayRulesScreen();

            //
            // Main control loop for game.
            //
            while (playingGame)
            {
                InitializeRound();
                while (!playingRound)
                {
                    DisplayGetPlayerGuessScreen();
                    DisplayPlayerGuessFeedback();
                    UpdateAndDisplayRoundStatus();
                    
                }
            //
            // There is a problem here. It does not enter into playing another round.
            // It just keeps displaying the Player stats over and over angin. Likewise,
            // if you type "no" in response to the question of whether or not you want to play 
            // again it also just displays the stats again. If I move it up to inside the inner
            // while loop above it behaves very strange. A little help would be appriciated.
            DisplayPlayerStats();    
            }
            DisplayClosingScreen();
        }

        public static void InitializeRound()
        {
            DisplayReset();
            numberToGuess = GetNumberToGuess();
        }

        public static int GetNumberToGuess()
        {
            return random.Next(0, 11);
        }
        
        public static int DisplayGetPlayerGuessScreen()
        {
            //
            // 
            //
            Console.WriteLine("Enter the number of your guess as an integer: ");
            int.TryParse(Console.ReadLine(), out playersGuess);
            numbersPlayerHasGuessed[roundNumber] = playersGuess;

            //Console.WriteLine("Player guess from array: " + numbersPlayerHasGuessed[roundNumber]);
            return playersGuess;
        }
        
        public static void DisplayPlayerGuessFeedback()
        {
            DisplayReset();
            roundStats();
            if (roundNumber < MAX_NUMBER_OF_PLAYER_GUESSES - 1)
            {
                if (playersGuess == numberToGuess)
                {
                    Console.WriteLine("You guessed: " + playersGuess);
                    Console.WriteLine("Congratulations! You guessed the secret number.");                    
                    DisplayContinuePrompt();
                    playingRound = true;
                }
                else if (playersGuess < numberToGuess)
                {
                    Console.WriteLine("I'm sorry. You guessed too low.");
                    DisplayContinuePrompt();
                }
                else
                {
                    Console.WriteLine("I'm sorry. You guessed too high.");
                    DisplayContinuePrompt();
                }
            }
            else
            {
                if (playersGuess == numberToGuess)
                {
                    Console.WriteLine("You guessed: " + playersGuess);
                    Console.WriteLine("Congratulations! You guessed the secret number.");
                    DisplayContinuePrompt();
                    playingRound = true;
                }
                else
                {
                    DisplayLoseScreen();
                    playingRound = true;
                }                               
            }           
        }
        
        public static void DisplayLoseScreen()
        {
            Console.WriteLine("I'm sorry you've run out of chances.");
            DisplayContinuePrompt();
        }

        public static int UpdateAndDisplayRoundStatus()
        {
            numberOfCurrentPlayerGuess += 1;
            //Console.WriteLine($"Number of current guess: {numberOfCurrentPlayerGuess}");
                if ((playersGuess == numberToGuess))
                {
                    numberOfWins += 1;               
                    playingRound = true;
                }
                if (roundNumber < MAX_NUMBER_OF_PLAYER_GUESSES - 1)
                {

                }
                else
                {
                    playingRound = true;
                    numberOfLosses += 1;
                
                }
            roundNumber += 1;
            roundStats();
            return roundNumber;
        }

        public static void roundStats()
        {
            DisplayReset();
            Console.WriteLine("Rounds Played: " + roundNumber);
            Console.WriteLine("Current Random number: " + numberToGuess);
            DisplayNumbersGuessed();
        }

        public static void DisplayContinuePrompt()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void DisplayPlayerStats()
        {
            int total;
            double percentageOfWins;
            DisplayReset();
            Console.WriteLine($"Round number: {roundNumber}");
            Console.WriteLine($"Number of wins: {numberOfWins}");
            Console.WriteLine($"Number of losses: {numberOfLosses}");

            //
            // Parse the following output to display the percentage.
            //
            total = numberOfWins + numberOfLosses;
            percentageOfWins = (double)numberOfWins/total;
            Console.WriteLine($"Percentage of wins: {percentageOfWins}");
            DisplayContinuePrompt();
            DisplayContinueQuitPrompt();
            
        }


        public static void DisplayClosingScreen()
        {
            DisplayReset();
            Console.WriteLine("Thank you for playing!");

            DisplayContinuePrompt();
        }

        public static void DisplayNumbersGuessed()
        {
            Console.Write("Player's Guesses: ");
            foreach (int playerGuess in numbersPlayerHasGuessed)
            {
                Console.Write(playerGuess + " ");
            }
            Console.WriteLine();
        }
        

        public static void DisplayContinueQuitPrompt()
        {
            string userResponse;
            DisplayReset();
            Console.WriteLine("Would you like to continue? YES or NO:");
            userResponse = Console.ReadLine().ToUpper();

            if (userResponse == "YES")
            {
                playingGame = true;
                resetNumbersPlayerHasGuessed();
                InitializeGame();
            }
            else
            {
                playingGame = false;
            }
            DisplayContinuePrompt();
        }

        public static void resetNumbersPlayerHasGuessed()
        {
            playingRound = false;
            playersGuess = 0;
            for (int i = 0; i <= (MAX_NUMBER_OF_PLAYER_GUESSES -1); i++)
            {
                numbersPlayerHasGuessed[i] = 0;
                //Console.WriteLine(numbersPlayerHasGuessed[i]);
            }

            //Console.WriteLine("Test:");
            //Console.WriteLine("Displaying elements of array after reset:");
            //DisplayNumbersGuessed();
            DisplayContinuePrompt();
        }

        public static void DisplayReset()
        {
            Console.Clear();
            Console.WriteLine("THE NUMBER GUESSING GAME");          
        }
    }
}
