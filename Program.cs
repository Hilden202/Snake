using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static bool isPaused = false; // Variabel för att hålla reda på om spelet är pausat

        static void Main(string[] args)
        {
            RestartGame(); // Starta spelet för första gången
        }

        public static void RestartGame()
        {
            //Console.Clear();
            Spelplan spelplan = new Spelplan(30, 15); // Exempelstorlek
            Skatten skatt = new Skatten(6, 2); // Sätt skattens startposition
            Snaken snake = new Snaken(2, 1); // Startposition för snaken
            // Skriv ut poäng

            GameLoop(spelplan, snake, skatt); // Starta spelet
        }

        static void GameLoop(Spelplan spelplan, Snaken snake, Skatten skatt)
        {
            while (true)
            {
                if (!isPaused) // Rita bara om spelet inte är pausat
                {
                    spelplan.RitaSpelplan(snake, skatt);
                    bool gameOver = snake.MoveAutomatically(spelplan, skatt);

                    if (gameOver)
                    {
                        Console.WriteLine("Spelet är slut! Tryck på valfri tangent för att starta om.");
                        Console.ReadKey(true);
                        RestartGame();
                        break;
                    }
                }

                // Kontrollera för tangenttryckningar
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    Direction direction = snake.CurrentDirection; // Standardriktning är nuvarande riktning

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Spacebar:
                            isPaused = !isPaused; // Växla pausläge
                            if (isPaused)
                            {
                                Console.SetCursorPosition(0, spelplan.VerticalWallLength + 1);
                                Console.WriteLine("Spelet är pausat. Tryck mellanslag eller piltangent för att fortsätta.");
                            }
                            else
                            {
                                Console.Clear();
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            direction = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            direction = Direction.Down;
                            break;
                        case ConsoleKey.LeftArrow:
                            direction = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            direction = Direction.Right;
                            break;

                        default:
                            continue; // Om annan tangent trycks, hoppa över iterationen
                    }

                    if (isPaused && keyInfo.Key != ConsoleKey.Spacebar) // Om spelet är pausat och en riktningstangent trycks
                    {
                        isPaused = false; // Avsluta pausläget
                        Console.Clear();
                    }

                    snake.ChangeDirection(direction);
                }

                if (!isPaused)
                {
                    Thread.Sleep(snake.GetSpeed());
                }
            }
        }
    }
}
