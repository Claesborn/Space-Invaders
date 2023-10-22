using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



static class Score
{
    public static int score { get; private set; }
    public static int highscore { get; private set; }

    static Score()
    {
        LoadHighScore();
    }

    public static void AddScore(int points)
    {
        score += points;
        if(score > highscore)
        {
            highscore = score;
            SaveHighScore();
        }
    }

    public static void DisplayScore()
    {
        Console.SetCursorPosition(Console.WindowWidth - 15, 0);
        Console.ResetColor();
        Console.Write($"Score: {score}");
        Console.SetCursorPosition(Console.WindowWidth - 15, 1);
        Console.Write($"Highscore: {highscore}");
    }

    private static void LoadHighScore()
    {
        if (File.Exists("highscore.txt"))
        {
            try
            {
                highscore = int.Parse(File.ReadAllText("highscore.txt"));
            }
            catch (FileLoadException e)
            {
                Console.WriteLine("Error loading highscore: " + e.Message);
            }
        }
    }

    public static void SaveHighScore()
    {
        try
        {
            File.WriteAllText("highscore.txt", highscore.ToString());
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("Error saving highscore: " + e.Message);
        }
    }
}

