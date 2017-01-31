using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNumberGuessingGame
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
        private static int gameNumber;
        private static int numberOfWins;
        private static int numberOfLosses;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];
        private static bool playingGame;
        private static bool playingRound;
        private static Random random = new Random();

        static void Main(string[] args)
        {
            playingGame = DisplayWelcomeScreen();            
                
            InitializeGame();      
            
            DisplayClosingScreen();
            
        }

        public static bool DisplayWelcomeScreen()
        {
            Console.WriteLine("Welcome to the Number Guessing Game!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //
            // ask user if they want to play the game, validate for yes/no
            //

            playingGame = ValidateYesNo("Would you like to play?");
            

            return playingGame;
        }

        private static bool ValidateYesNo(string prompt)
        {
            bool validResponse = false;
            bool userSaidYes = false;

            while (!validResponse)
            {
                DisplayReset();
                Console.Write($"{prompt} Type yes or no: ");
                //Console.WriteLine();
                string userResponse = Console.ReadLine().ToUpper();

                if (userResponse == "YES")
                {
                    userSaidYes = true;
                    validResponse = true;
                }
                else if (userResponse == "NO")
                {
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine("Please type exactly \"yes\" or \"no\".");
                    validResponse = false;
                }
            }

            return userSaidYes;
        }

        public static void DisplayRulesScreen()
        {
            Console.WriteLine("The computer will choose a secret number between 1 and 10.");
            Console.WriteLine("The user will guess one integer at a time, up to four times");
            Console.WriteLine("The computer will display if the guess is too low, too high");
            Console.WriteLine("or correct.");
            Console.WriteLine();
            DisplayContinuePrompt();

            DisplayReset();
            

        }

        public static void InitializeGame()
        {
            //
            // Main control loop for game
            //

            while (playingGame)
            {
                DisplayReset();

                //
                // Count the games so that rule screen is only shown for first game
                //

                gameNumber += 1;

                if (gameNumber <= 1)
                {
                    DisplayRulesScreen();
                }           

                InitializeRound();
                
                playingRound = true;
                
                for (int round = 0; round < MAX_NUMBER_OF_PLAYER_GUESSES; round++)
                {//cycles through four rounds, unless playingRound becomes false
                 //(if they win or choose to quit)
                    if (playingRound)
                    {
                        DisplayGetPlayerGuessScreen();

                        DisplayPlayerGuessFeedback();

                        UpdateAndDisplayRoundStatus();
                        
                        //DisplayContinueQuitRoundPrompt();
                    }                    
                }


                DisplayReset();
                DisplayContinueQuitGamePrompt();
                                
            }
        }

        public static void InitializeRound()
        {
            numberToGuess = GetNumberToGuess();

            roundNumber = 1;

            //
            // reset the array to zero, so that round 2 and up have clean slates
            //            

            for (int index = 0; index < MAX_NUMBER_OF_PLAYER_GUESSES; index++)
            {
                numbersPlayerHasGuessed[index] = 0;
            }
            
        }
        
        public static int GetNumberToGuess()
        {            
            return random.Next(1, MAX_NUMBER_TO_GUESS);
        }
        
        public static int DisplayGetPlayerGuessScreen()
        {
            DisplayReset();

            //
            // Display previous guesses for rounds 2 and up
            //

            //if (roundNumber > 1)
            //{
            //    DisplayNumbersGuessed();
            //}

            //
            // ask player for guess and validate for integer between 1 and 10
            //

            playersGuess = ValidateInteger("the number of your guess", 1, 10);
            

            //
            // store player guess in array
            //

            numbersPlayerHasGuessed[roundNumber - 1] = playersGuess;
            //"roundNumber - 1" allows the index to use the 0 start
            //while letting the user see the roundNumber as 1 start

            //
            // display player's guess to them
            //

            Console.WriteLine($"Your guess: {numbersPlayerHasGuessed[roundNumber - 1]}");
            

            return playersGuess;
        }

        private static int ValidateInteger(string integerNeeded, int minValue, int maxValue)
        {
            bool validResponse = false;

            string userResponse;
            int userInteger = 0;

            while (!validResponse)
            {
                DisplayReset();
                DisplayNumbersGuessed();
                Console.Write($"Please enter {integerNeeded} as an integer ({minValue} - {maxValue}): ");
                userResponse = Console.ReadLine();
                if (int.TryParse(userResponse, out userInteger))
                {
                    if (userInteger >= minValue && userInteger <= maxValue)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        DisplayReset();
                        Console.WriteLine($"Please enter a valid integer between {minValue} and {maxValue}!");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    DisplayReset();
                    Console.WriteLine($"Please only enter a whole number between {minValue} and {maxValue}!");
                    DisplayContinuePrompt();
                    Console.Clear();                    
                }
                
            }

            return userInteger;
        }

        public static void DisplayPlayerGuessFeedback()
        {
            DisplayReset();
            
            if (playersGuess == numberToGuess)
            {
                Console.WriteLine($"That's right! The number was {numberToGuess}");
            }
            else if (playersGuess < numberToGuess)
            {
                Console.WriteLine($"Sorry, {playersGuess} is too low.");
            }
            else
            {
                Console.WriteLine($"Sorry, {playersGuess} is too high.");
            }
        }

        public static int UpdateAndDisplayRoundStatus()
        {
            if (playersGuess == numberToGuess)
            {                
                playingRound = false;
                Console.WriteLine("Increasing your number of wins by 1.");
                Console.WriteLine();
                numberOfWins += 1;
            }
            else if (playersGuess != numberToGuess && roundNumber < MAX_NUMBER_OF_PLAYER_GUESSES)
            {
                numbersPlayerHasGuessed[roundNumber - 1] = playersGuess;
                Console.WriteLine("Storing your incorrect guess in array.");
                Console.WriteLine();
                roundNumber += 1;
            }
            else if (playersGuess != numberToGuess && roundNumber >= MAX_NUMBER_OF_PLAYER_GUESSES)
            {
                playingRound = false;
                Console.WriteLine("Guesses used up. Ending game.");
                Console.WriteLine("Increasing your number of losses by 1.");
                Console.WriteLine();
                numberOfLosses += 1;
            }
            DisplayContinuePrompt();
            return roundNumber;
        }

        public static void DisplayRoundStats()
        {
            DisplayReset();
            Console.WriteLine($"Rounds Completed: {roundNumber - 1}");
            DisplayNumbersGuessed();
        }

        public static void DisplayPlayerStats()
        {
            DisplayReset();
            
            Console.WriteLine($"Number of Wins: {numberOfWins}");
            Console.WriteLine($"Number of Losses: {numberOfLosses}");

            //
            // Calculate and display percentage of wins:
            //

            int total = numberOfLosses + numberOfWins;
            double percentageOfWins = (double)numberOfWins / total;
            Console.WriteLine($"Percentage of wins: {percentagizer(percentageOfWins)}%");
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();

        }
        //
        // Return a percentage from a decimal value
        //
        private static double percentagizer(double percent)
        {
            return percent *= 100.0;
        }

        public static void DisplayContinueQuitRoundPrompt()
        {            
            if (playingRound) //this "if" statement will stop the game from asking if they want to  
                              //play another round if they already lost the game or won the game
            {
                DisplayReset();

                //
                // ask and validate if user wants to play another round.
                //

                bool continueRound = ValidateYesNo("Would you like to play another round?");

                if (continueRound)
                {
                    playingRound = true; //updating playingRound here allows the player to move
                                         //on to the next round
                                         //I know, it's silly to define it twice. But this way,
                                         //it is checking both for player's CHOICE to move on or not
                                         //as well as the ABILITY to move on or not based on
                                         //if they have already lost or won, as calculated in
                                         //a previous method
                }
                else
                {
                    playingRound = false;
                    playingGame = false;
                }              
               
            }
        }
        public static void DisplayClosingScreen()
        {
            DisplayReset(); 

            Console.WriteLine("Thank you for playing!");

            DisplayContinuePrompt();
        }

        public static void DisplayNumbersGuessed()
        {
            Console.Write($"Previous guesses: ");
            for (int round = 0; round < roundNumber - 1; round++)
            {
                Console.Write(numbersPlayerHasGuessed[round] + " ");
            }
            Console.WriteLine();
        }
        public static void DisplayReset()
        {
            Console.Clear();
            Console.WriteLine("The Number Guessing Game");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void DisplayContinueQuitGamePrompt()
        {
            DisplayReset();

            DisplayPlayerStats();

            playingGame = ValidateYesNo("Would you like to play another game?");
        }

        public static void DisplayContinuePrompt()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        
        
    }
}