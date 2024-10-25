using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{
    class Program
    {
        static int size = 10;
        static Snaken snake;
        static int skattX = 1;
        static int skattY = 1;
        static Random random = new Random();
        static Direction direction;

        enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        static void Main(string[] args)
        {
            while (true)
            {
                StartaOmSpelet();
                if (!SpelaSpelet()) break; // Avsluta om användaren väljer att göra det
            }
        }

        static void StartaOmSpelet()
        {
            snake = new Snaken(size); // Skapa en ny instans av Snaken
            direction = Direction.None; // Ingen riktning i början
            FlyttaSkatt(); // Initiera skatten
        }

        static bool SpelaSpelet()
        {
            RitaSpelplan();

            // Vänta på att användaren trycker på en tangent för att starta
            while (direction == Direction.None)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    direction = key.Key switch
                    {
                        ConsoleKey.UpArrow => Direction.Up,
                        ConsoleKey.DownArrow => Direction.Down,
                        ConsoleKey.LeftArrow => Direction.Left,
                        ConsoleKey.RightArrow => Direction.Right,
                        _ => direction // Behåll tidigare riktning om ingen giltig tangent trycks
                    };
                }
            }

            int maxScore = (size * size) - 1; // Beräkna maximal poäng

            // Loop för att flytta snaken
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Avsluta programmet
                    if (key.KeyChar == 'q')
                    {
                        return false; // Avsluta spelet
                    }

                    // Ändra riktning, men förhindra motsatt riktning
                    Direction newDirection = key.Key switch
                    {
                        ConsoleKey.UpArrow => Direction.Up,
                        ConsoleKey.DownArrow => Direction.Down,
                        ConsoleKey.LeftArrow => Direction.Left,
                        ConsoleKey.RightArrow => Direction.Right,
                        _ => direction // Behåll tidigare riktning om ingen giltig tangent trycks
                    };

                    // Blockera motsatt riktning
                    if ((direction == Direction.Up && newDirection != Direction.Down) ||
                        (direction == Direction.Down && newDirection != Direction.Up) ||
                        (direction == Direction.Left && newDirection != Direction.Right) ||
                        (direction == Direction.Right && newDirection != Direction.Left))
                    {
                        direction = newDirection;
                    }
                }

                // Flytta snaken i vald riktning
                (int newX, int newY) = snake.Snake[0]; // Hämta den nuvarande positionen för snaken

                switch (direction)
                {
                    case Direction.Up:
                        newY = (newY > 0) ? newY - 1 : size - 1; // Hoppa till botten om snaken går upp
                        break;
                    case Direction.Down:
                        newY = (newY < size - 1) ? newY + 1 : 0; // Hoppa till toppen om snaken går ner
                        break;
                    case Direction.Left:
                        newX = (newX > 0) ? newX - 1 : size - 1; // Hoppa till höger om snaken går vänster
                        break;
                    case Direction.Right:
                        newX = (newX < size - 1) ? newX + 1 : 0; // Hoppa till vänster om snaken går höger
                        break;
                    default:
                        continue; // Om ingen giltig riktning, hoppa till nästa iteration
                }

                // Kontrollera om snaken har nått skatten
                if (newX == skattX && newY == skattY)
                {
                    snake.Vaxa();
                    FlyttaSkatt();

                    // Kontrollera om spelaren når maximal poäng
                    if (snake.Score >= maxScore)
                    {
                        DuVann();
                        break; // Avsluta spelet
                    }
                }

                // Uppdatera snaken med den nya positionen
                snake.Flytta(newX, newY);

                // Kontrollera om snaken har krockat med sig själv
                if (snake.KrockaMedSigSjälv())
                {
                    GameOver();
                    break; // Avsluta spelet
                }

                RitaSpelplan();
                Thread.Sleep(snake.SleepTime); // Använd den aktuella fördröjningen
            }

            return true; // Återvänd till huvudloopen för att starta om spelet
        }

        static void FlyttaSkatt()
        {
            int newX, newY;

            do
            {
                newX = random.Next(0, size);
                newY = random.Next(0, size);
            }
            while (snake.Snake.Contains((newX, newY))); // Se till att skatten inte hamnar på snaken

            skattX = newX;
            skattY = newY;
        }

        static void RitaSpelplan()
        {
            Console.Clear();
            Console.WriteLine("Poäng: " + snake.Score);
            Console.WriteLine();

            Tak();

            for (int i = 0; i < size; i++)
            {
                Väggar(i);
                if (i == size - 1)
                {
                    Golv();
                }
            }
        }

        static void Tak()
        {
            Console.WriteLine(new string('—', size * 3 + 2));
        }

        static void Väggar(int currentY)
        {
            Console.Write("|"); // vänster vägg

            for (int spelplan = 0; spelplan < size; spelplan++)
            {
                // Rita snaken
                if (snake.Snake[0] == (spelplan, currentY))
                {
                    Console.Write(" O "); // Rita huvudet
                }
                else if (snake.Snake.Contains((spelplan, currentY)))
                {
                    Console.Write(" o "); // Rita kroppen
                }

                else if (currentY == skattY && spelplan == skattX)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" ■ ");  // Rita skatt
                    Console.ResetColor();  // Återställer standardfärgen
                }
                else
                {
                    Console.Write("   ");
                }
            }

            Console.WriteLine("|");
        }

        static void Golv()
        {
            Console.WriteLine(new string('—', size * 3 + 2));
        }

        static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game Over! Försök igen.");
            Thread.Sleep(2000);
            Console.WriteLine("Tryck på valfri tangent för att börja om...");

            // Vänta på att användaren trycker på en tangent
            while (!Console.KeyAvailable) { } // Håll loop tills en tangent trycks
            Console.ReadKey(true); // Läs tangenttrycket utan att visa det
        }

        static void DuVann()
        {
            Console.Clear();
            Console.WriteLine("Du vann! Du nådde maximal poäng.");
            Thread.Sleep(2000);
            Console.WriteLine("Tryck på valfri tangent för att börja om...");
            while (!Console.KeyAvailable) { } // Håll loop tills en tangent trycks
            Console.ReadKey(true); // Läs tangenttrycket utan att visa det
        }
    }
}
