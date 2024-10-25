using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            RestartGame(); // Starta spelet för första gången
        }

        public static void RestartGame() // Gör denna metod public
        {
            // Återställ spelets tillstånd
            Console.Clear();
            Spelplan spelplan = new Spelplan(30, 15); // Exempelstorlek
            Skatten skatt = new Skatten(6, 2); // Sätt skattens startposition
            Snaken snake = new Snaken(2, 1); // Startposition för snaken

            GameLoop(spelplan, snake, skatt); // Starta spelet igen
        }

        static void GameLoop(Spelplan spelplan, Snaken snake, Skatten skatt)
        {
            while (true)
            {
                spelplan.RitaSpelplan(snake, skatt);
                bool gameOver = snake.MoveAutomatically(spelplan, skatt); // Kolla om spelet ska avslutas
                snake.PrintScore();

                if (gameOver)
                {
                    Console.WriteLine("Spelet är slut! Tryck på valfri tangent för att starta om.");
                    Console.ReadKey(true); // Väntar på en tangenttryckning
                    RestartGame(); // Starta om spelet
                    break; // Avsluta den nuvarande loopen
                }

                // Kontrollera för tangenttryckningar
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Väntar på tangenttryckning
                    Direction direction = Direction.Right; // Default riktning

                    // Ställ in riktningen baserat på tangenttryckning
                    switch (keyInfo.Key)
                    {
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

                    snake.ChangeDirection(direction); // Ändra riktning
                }

                Thread.Sleep(snake.GetSpeed()); // Väntar en stund innan nästa rörelse
            }
        }
    }
}
