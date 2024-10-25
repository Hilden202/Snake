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
            Console.Clear();
            for (int y = 0; y <= VerticalWallLength; y++)
            {
                for (int x = 0; x <= HorisontalWallLength; x++)
                {
                    if (x == 0 || x == HorisontalWallLength) // Väggar
                    {
                        Console.Write("|");
                    }
                    else if (y == 0 || y == VerticalWallLength) // Tak och golv
                    {
                        Console.Write("-");
                    }
                    else if (snake.Snake.Contains((x, y))) // Snaken
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ResetColor();
                    }
                    else if (skatt.Skatt == (x, y)) // Skatt
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("■");
                        Console.ResetColor();
                    }
                    else // Tomt fält
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
