﻿using System;
using static System.Formats.Asn1.AsnWriter;

namespace Snake
{
    public class Spelplan
    {
        public int HorisontalWallLength { get; private set; }
        public int VerticalWallLength { get; private set; }
        private ToppListan toppListan;

        public Spelplan(int horisontalSize, int verticalSize, ToppListan toppListan)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
            this.toppListan = toppListan; // Spara instansen av ToppListan
        }

        public void RitaSpelplan(Snaken snake, Skatten skatt)
        {
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
            snake.RitaSnaken();

            // Rita skatten
            skatt.RitaSkatten();


            // Anropa RitaTopplista för att visa topp 10-listan
            toppListan.RitaTopplista();


            // Skriv ut poäng
            Console.SetCursorPosition(1, VerticalWallLength + 1); // Sätt position för poäng (exempelvis 2, 0)
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Poäng: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(toppListan.GetCurrentScore()); // Skriv ut poängen
            Console.ResetColor();
            Console.SetCursorPosition(0, 16);
        }
    }
}
