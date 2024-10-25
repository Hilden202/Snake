using System;
using System.Collections.Generic;

namespace Snake
{
    public class Snaken
    {
        public List<(int x, int y)> Snake { get; private set; } = new List<(int, int)>();
        public int Score { get; private set; }
        public int SleepTime { get; private set; } = 250; // Initial sleep time
        public int Size { get; private set; }

        public Snaken(int size)
        {
            Size = size;
            Snake.Add((0, 0)); // Snaken börjar med en del
            Score = 0;
        }

        public void Flytta(int newX, int newY)
        {
            for (int i = Snake.Count - 1; i > 0; i--)
            {
                Snake[i] = Snake[i - 1]; // Flytta varje del till föregående position
            }
            Snake[0] = (newX, newY); // Sätt den nya positionen för huvudet
        }

        public void Vaxa()
        {
            Snake.Add(Snake[Snake.Count - 1]); // Duplicera den sista delen för att växa
            Score++;
            SleepTime = Math.Max(50, SleepTime - 2); // Minska sleep time, men inte under 50 ms
        }

        public bool KrockaMedSigSjälv()
        {
            var head = Snake[0];
            return Snake.GetRange(1, Snake.Count - 1).Contains(head);
        }

        public void Rensa()
        {
            Snake.Clear();
            Snake.Add((0, 0)); // Återställ snaken
            Score = 0;
            SleepTime = 250; // Återställ sleep time
        }
    }
}

