using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Game
{
    Player player;
    Stopwatch stopwatch = new();
    double deltaTime = 0;
    Enemy[] enemies = new Enemy[20];
    int maxEnemies = 20;
    int enemyCount = 0;
    double tick = 3;
    double spawnTimer = 3;
    static Random random = new();

    double frameTime = 1.0 / 25;
    public Game()
    {
        
    }
    public void Run()
    {
        Init();
        while (true)
        {
            stopwatch.Restart();
            Update(deltaTime);
            Render();
            
            deltaTime = stopwatch.Elapsed.TotalSeconds;
            if (deltaTime < frameTime)
            {
                int sleepTime = (int)(frameTime - deltaTime) * 1000;
                if (sleepTime > 0)
                    Thread.Sleep(sleepTime);
            }
        }
    }
    void Init()
    {
        Console.Title = "Space Invaders Ultimate Version";
        player = new(3, Console.WindowWidth / 2, Console.WindowHeight - 2);
        Console.CursorVisible = false;
        stopwatch.Start();
    }
    void Update(double deltaTime)
    {
        player.HandlePlayerInput();
        foreach (Bullet bullet in player.bullets)
        {
            if (bullet != null)
                bullet.UpdateBullet(deltaTime);
        }
        CheckDestruction();
        SpawnEnemies();
        foreach (Enemy enemy in enemies)
        {
            if(enemy != null)
            {
                enemy.Move(deltaTime);
            }
        }

    }

    void Render()
    {
        Console.Clear();

        if (player.isAlive)
        {
            player.Draw();
        }
        foreach (Bullet bullet in player.bullets)
        {
            if(bullet != null)
                bullet.Draw();
        }
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                foreach (Bullet bullet in enemy.bullets)
                {
                    if (bullet != null)
                        bullet.Draw();
                }
            }
            if(enemy != null && enemy.isAlive)
            {
                enemy.Draw();
            }
        }
    }
    void CheckDestruction()
    {
        player.HandleBulletDestruction();
        player.Destroy();
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.HandleBulletDestruction();
                enemy.Destroy();
            }
        }
    }

    void SpawnEnemies()
    {
        tick -= deltaTime;

        if(tick <= 0)
        {
            if (enemyCount < maxEnemies)
            {
                tick += spawnTimer;
                enemies[enemyCount] = new Enemy(1, random.Next(1, Console.WindowWidth), 1);
                enemyCount++;
            }
        }
    }
}
