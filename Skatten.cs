using System;
using System.Collections.Generic;

namespace Snake
{
    public class Skatten
    {
        public (int x, int y) Skatt { get; private set; } // För att hålla reda på skattens position
        public int Score { get; set; }

        // Konstruktor för att sätta skattens position
        public Skatten(int x, int y)
        {
            Skatt = (x, y);
            Score = 0;
        }
        public int GetScore() // Ny metod för att få poängen
        {
            return Score;
        }

        public void RitaSkatten()
        {
            Console.SetCursorPosition(Skatt.Item1, Skatt.Item2); // Ställ in cursorposition för skatten
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("■");
            Console.ResetColor();
        }

        public void FlyttaSkatt(Snaken snake, Spelplan spelplan)
        {
            Random random = new Random();
            int newX, newY;

            // Skapa en lista med giltiga positioner för skatten
            List<(int, int)> giltigaPositioner = new List<(int, int)>();

            for (int x = 2; x < spelplan.HorisontalWallLength; x += 2)
            {
                for (int y = 2; y < spelplan.VerticalWallLength; y += 2)
                {
                    if (!snake.Snake.Contains((x, y))) // Kolla att positionen inte är en del av snaken
                    {
                        giltigaPositioner.Add((x, y));
                    }
                }
            }

            if (giltigaPositioner.Count > 0)
            {
                (newX, newY) = giltigaPositioner[random.Next(giltigaPositioner.Count)];
                Skatt = (newX, newY);
            }
        }
    }
}
