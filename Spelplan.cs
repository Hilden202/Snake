using System;

namespace Snake
{
    public class Spelplan
    {
        public int HorisontalWallLength { get; private set; }
        public int VerticalWallLength { get; private set; }

        public Spelplan(int horisontalSize, int verticalSize)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
        }

        public void RitaSpelplan(Snaken snake, Skatten skatt)
        {
            // Rensa konsolen för att börja om
            Console.Clear();

            // Rita väggarna och tomma fält
            for (int y = 0; y <= VerticalWallLength; y++)
            {
                for (int x = 0; x <= HorisontalWallLength; x++)
                {
                    Console.SetCursorPosition(x, y); // Ställ in cursorposition

                    if (x == 0 || x == HorisontalWallLength) // Väggar
                    {
                        Console.Write("|");
                    }
                    else if (y == 0 || y == VerticalWallLength) // Tak och golv
                    {
                        Console.Write("-");
                    }
                    else // Tomt fält
                    {
                        Console.Write(" ");
                    }
                }
            }

            // Rita snaken
            foreach (var segment in snake.Snake)
            {
                Console.SetCursorPosition(segment.Item1, segment.Item2); // Ställ in cursorposition för snaken
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("O");
                Console.ResetColor();
            }

            // Rita skatten
            Console.SetCursorPosition(skatt.Skatt.Item1, skatt.Skatt.Item2); // Ställ in cursorposition för skatten
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("■");
            Console.ResetColor();

            // Skriv ut poäng
            Console.SetCursorPosition(2, 0); // Sätt position för poäng (exempelvis 2, 0)
            Console.ForegroundColor = ConsoleColor.White;
            //Console.Write(snake.GetScore()); // Skriv ut poängen
            Console.ResetColor();
        }
    }
}
