using System;
using System.Threading;
using System.Xml.Linq;

namespace Snake
{
    class Program
    {
        static bool isPaused = false; // Variabel för att hålla reda på om spelet är pausat

        static void Main(string[] args)
        {
            isPaused = true;
            RestartGame(); // Starta spelet för första gången
        }


        public static void RestartGame()
        {
            Console.Clear();
            ToppListan toppListan = new ToppListan(); // Skapa en instans av ToppLis
            Spelplan spelplan = new Spelplan(30, 15, toppListan); // Exempelstorlek
            Skatten skatt = new Skatten(6, 2); // Sätt skattens startposition
            Snaken snake = new Snaken(2, 1, toppListan); // Startposition för snaken
            //GameLoop(spelplan, snake, skatt, toppListan); // Starta spelet

            spelplan.RitaSpelplan(snake, skatt);

            isPaused = true; // Starta spelet i pausat läge
            Console.SetCursorPosition(0, spelplan.VerticalWallLength + 2); // Positionera cursor för meddelande
            Console.WriteLine("Spelet är pausat. Tryck mellanslag eller piltangent för att börja spela.");

            GameLoop(spelplan, snake, skatt, toppListan); // Starta spelet
        }

        static void GameLoop(Spelplan spelplan, Snaken snake, Skatten skatt, ToppListan toppListan)
        {
            while (true)
            {
                if (!isPaused) // Rita bara om spelet inte är pausat
                {
                    spelplan.RitaSpelplan(snake, skatt);
                    bool gameOver = snake.MoveAutomatically(spelplan, skatt);

                    if (gameOver)
                    {
                        Console.Write("Spelet är slut! Ange ditt namn: ");
                        string name = Console.ReadLine();
                        // Anropa SaveScore med användarens namn
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            RestartGame();
                            break;
                        }

                        toppListan.SaveScore(name);

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
                                Console.SetCursorPosition(0, spelplan.VerticalWallLength + 2);
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

                    // Ändra snaken riktning direkt här
                    snake.ChangeDirection(direction);
                }

                // Sedan kan du ha sömnperioden här nere
                if (!isPaused)
                {
                    Thread.Sleep(snake.GetSpeed());
                }
            }
        }
    }
}
