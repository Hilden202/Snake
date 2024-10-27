using System;
using System.Collections.Generic;

namespace Snake
{
    public class Snaken
    {

        public List<(int x, int y)> Snake { get; private set; }
        public Direction CurrentDirection { get; private set; } // Offentlig variabel för nuvarande riktning
        private int speed; // Hastighet
        private ToppListan toppListan;

        public Snaken(int initialX, int initialY, ToppListan toppListan)
        {
            Snake = new List<(int, int)> { (initialX, initialY) };
            speed = 150;
            CurrentDirection = Direction.Right;
            this.toppListan = toppListan; // Spara instansen av ToppListan
            Console.OutputEncoding = System.Text.Encoding.UTF8;

        }

        public void RitaSnaken()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var segment in Snake)
            {
                Console.SetCursorPosition(segment.x, segment.y);
                Console.Write("◉");
            }
            Console.ResetColor();
        }

        public bool MoveAutomatically(Spelplan spelplan, Skatten skatt)
        {
            return Move(CurrentDirection, spelplan, skatt); // Returnera resultatet från Move
        }

        public bool Move(Direction direction, Spelplan spelplan, Skatten skatt)
        {
            var head = Snake[0];

            // Flytta huvudet baserat på den aktuella riktningen
            switch (CurrentDirection)
            {
                case Direction.Up:
                    head.y = (head.y > 1) ? head.y - 1 : spelplan.VerticalWallLength - 1;
                    break;
                case Direction.Down:
                    head.y = (head.y < spelplan.VerticalWallLength - 1) ? head.y + 1 : 1;
                    break;
                case Direction.Left:
                    head.x = (head.x > 2) ? head.x - 2 : spelplan.HorisontalWallLength - 2;
                    break;
                case Direction.Right:
                    head.x = (head.x < spelplan.HorisontalWallLength - 2) ? head.x + 2 : 2;
                    break;
            }

            // Kolla om snaken har samlat in skatten
            if (head == skatt.Skatt)
            {
                toppListan.AddScore();
                speed = Math.Max(50, speed - 1);
                skatt.FlyttaSkatt(this, spelplan);
            }
            else
            {
                Snake.RemoveAt(Snake.Count - 1); // Ta bort den sista segmentet om inget samlades in
            }

            // Kontrollera om snaken krockar med sig själv
            if (Snake.Contains(head))
            {
                return true; // Spelet ska avslutas
            }

            Snake.Insert(0, head); // Lägg till huvudet
            return false; // Spelet fortsätter
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public void ChangeDirection(Direction direction)
        {
            // Endast tillåtna riktningar
            if ((CurrentDirection == Direction.Up && direction != Direction.Down) ||
                (CurrentDirection == Direction.Down && direction != Direction.Up) ||
                (CurrentDirection == Direction.Left && direction != Direction.Right) ||
                (CurrentDirection == Direction.Right && direction != Direction.Left))
            {
                CurrentDirection = direction; // Ändra riktning om det inte är motsatt riktning
            }
        }

        public int GetSpeed() // Ny metod för att få hastigheten
        {
            return speed;
        }
    }
}
