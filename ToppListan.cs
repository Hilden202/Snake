using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snake
{
    public class ToppListan
    {
        private int currentScore;
        private readonly string filePath = "highscores.txt";

        public ToppListan()
        {
            currentScore = 0;
        }

        // Öka poängen när skatten samlas in
        public void AddScore()
        {
            currentScore++;
        }

        // Returnerar den aktuella poängen
        public int GetCurrentScore()
        {
            return currentScore;
        }

        // Laddar topp 10 poäng från fil
        public List<(string name, int score)> LoadScores()
        {
            List<(string name, int score)> scores = new List<(string, int)>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int score))
                    {
                        scores.Add((parts[0].Trim(), score));
                    }
                }
            }

            return scores.OrderByDescending(s => s.score).Take(10).ToList();
        }

        // Spara ny poäng till fil
        public void SaveScore(string name)
        {
            List<string> existingScores = LoadScores().Select(s => $"{s.name}: {s.score}").ToList();
            existingScores.Add($"{name}: {currentScore}");
            File.WriteAllLines(filePath, existingScores);
        }

        // Rita topp 10-listan
        public void RitaTopplista()
        {
            Console.SetCursorPosition(32, 0);
            Console.WriteLine("Topp Listan");
            Console.SetCursorPosition(32, 1);
            Console.WriteLine("------------");

            var topScores = LoadScores();
            for (int i = 0; i < topScores.Count; i++)
            {
                Console.SetCursorPosition(32, i + 2);

                // Set color for the top name
                if (i == 0) // Assuming i == 0 is the top player
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Set color for the top name
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White; // Set color for other names
                }

                Console.WriteLine($"{i + 1}. {topScores[i].name}: {topScores[i].score}p");
                Console.ResetColor(); // Reset color to default after printing
            }
        }

    }
}
