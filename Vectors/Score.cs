using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



static class Score
{
    static int score = 0;
    static int highscore = 0;

    public static void AddScore(int addScore)
    {
        score += addScore;
    }

    public static void DisplayScore()
    {
        Console.SetCursorPosition(Console.WindowWidth - 15, 0);
        Console.ResetColor();
        Console.Write($"Score: {score}");
    }
}

