using System;
using System.Collections.Generic;

namespace Snake
{
    public class Snaken
    {
        public List<(int x, int y)> Snake { get; private set; }
        public Direction CurrentDirection { get; private set; } // Offentlig variabel för nuvarande riktning
        private int score;
        private int speed; // Hastighet

        public Snaken(int initialX, int initialY)
        {
            Snake = new List<(int, int)> { (initialX, initialY) }; // Starta snaken på en position
            score = 0;
            speed = 250; // Starta med en hastighet på 250 ms
            CurrentDirection = Direction.Right; // Starta med en standardriktning
        }

        public int GetScore() // Ny metod för att få poängen
        {
            return score;
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
                    head.x = (head.x > 1) ? head.x - 2 : spelplan.HorisontalWallLength - 2;
                    break;
                case Direction.Right:
                    head.x = (head.x < spelplan.HorisontalWallLength - 2) ? head.x + 2 : 2;
                    break;
            }

            // Kolla om snaken har samlat in skatten
            if (head == skatt.Skatt)
            {
                score++;
                speed = Math.Max(200, speed - 5);
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

        public void PrintScore()
        {
            Console.WriteLine($"Poäng: {score}");
        }

        public int GetSpeed() // Ny metod för att få hastigheten
        {
            return speed;
        }
    }
}
