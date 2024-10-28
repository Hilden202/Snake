using System;
using System.Threading;

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
            ToppListan toppListan = new ToppListan(); // Skapa en instans av ToppLista
            Spelplan spelplan = new Spelplan(30, 15, toppListan); // Exempelstorlek
            Spelplan spelplan1 = new Spelplan(30, 30, toppListan);
            Skatten skatt = new Skatten(6, 2); // Sätt skattens startposition
            Snaken snake = new Snaken(2, 1, toppListan); // Startposition för snaken
            //GameLoop(spelplan, snake, skatt, toppListan); // Starta spelet

            spelplan.RitaSpelplan(snake, skatt);


            isPaused = true; // Starta spelet i pausat läge
            Console.SetCursorPosition(0, spelplan.VerticalWallLength + 2); // Positionera cursor för meddelande
            Console.WriteLine("Spelet är pausat. Välj en riktning för att börja spela.");

            GameLoop(spelplan, snake, skatt, toppListan); // Starta spelet
        }

        static void GameLoop(Spelplan spelplan, Snaken snake, Skatten skatten, ToppListan toppListan)
        {
            while (true)
            {
                if (!isPaused) // Rita bara om spelet inte är pausat
                {
                    spelplan.RitaSpelplan(snake, skatten);
                    bool gameOver = snake.MoveAutomatically(spelplan, skatten);

                    if (gameOver)
                    {
                        Console.SetCursorPosition(spelplan.HorisontalWallLength / 2 - 5, spelplan.VerticalWallLength);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Game over!");
                        Console.ResetColor();

                        // Kolla om poängen är tillräckligt hög för topplistan
                        if (toppListan.IsScoreQualifiedForTopList())
                        {
                            Console.SetCursorPosition(0, spelplan.VerticalWallLength + 3);
                            Console.Write("Grattis, du är topp 10! Ange namn om du vill stå med: ");
                            string name = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                toppListan.SaveScoreIfQualified(name);
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, spelplan.VerticalWallLength + 3);
                            Console.WriteLine("Tyvärr, försök igen. Tryck på varlfi tangent för att starta om..");
                            Thread.Sleep(2000);
                            Console.ReadKey(true);
                        }

                        RestartGame();
                        break;
                    }
                }

                // Kontrollera för tangenttryckningar
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    Snaken.Direction direction = snake.CurrentDirection; // Standardriktning är nuvarande riktning

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Spacebar:
                            isPaused = !isPaused; // Växla pausläge
                            if (isPaused)
                            {
                                Console.SetCursorPosition(0, spelplan.VerticalWallLength + 2);
                                Console.WriteLine("Spelet är pausat. Tryck mellanslag eller välj riktning för att fortsätta..");
                            }
                            else
                            {
                                // Rensa pausmeddelandet när spelet återupptas
                                Console.SetCursorPosition(0, spelplan.VerticalWallLength + 2);
                                Console.WriteLine(new string(' ', Console.WindowWidth));

                                Console.Clear(); // Rensa hela skärmen
                                spelplan.RitaSpelplan(snake, skatten); // Rita om spelplanen
                            }
                            break;

                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:

                            if (isPaused)
                            {
                                isPaused = false; // Avsluta pausläget om spelet är pausat

                                // Rensa pausmeddelandet när spelet återupptas med piltangent
                                Console.SetCursorPosition(0, spelplan.VerticalWallLength + 2);
                                Console.WriteLine(new string(' ', Console.WindowWidth));

                                Console.Clear(); // Rensa hela skärmen
                                spelplan.RitaSpelplan(snake, skatten); // Rita om spelplanen
                            }

                            // Ändra riktning
                            direction = keyInfo.Key switch
                            {
                                ConsoleKey.UpArrow => Snaken.Direction.Up,
                                ConsoleKey.DownArrow => Snaken.Direction.Down,
                                ConsoleKey.LeftArrow => Snaken.Direction.Left,
                                ConsoleKey.RightArrow => Snaken.Direction.Right,
                                _ => direction
                            };
                            snake.ChangeDirection(direction);
                            break;

                        case ConsoleKey.Q:
                            // Avsluta spelet
                            Environment.Exit(0); // Avsluta programmet
                            break;

                        default:
                            continue; // Om annan tangent trycks, hoppa över iterationen
                    }
                }


                // Lägg till sömnperioden för att sakta ned spelet om det inte är pausat
                if (!isPaused)
                {
                    Thread.Sleep(snake.GetSpeed());
                }
            }
        }
    }
}
