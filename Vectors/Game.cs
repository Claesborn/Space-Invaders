
using System.Diagnostics;


class Game
{
    Player player;
    Stopwatch stopwatch = new();
    double deltaTime = 0;
    Enemy[] enemies = new Enemy[50];
    int maxEnemies = 20;
    int enemyCount = 0;
    double tick = 3;
    double spawnTimer = 3;
    static Random random = new();
    //Deathanimation[] deathanimations = new Deathanimation[20];
    //int animationCounter = 0;
    double frameTime = 1.0 / 25;
    public Game()
    {
        
    }
    public void Run()
    {
        Init();
        while (player.isAlive)
        {
            stopwatch.Restart();
            Update(deltaTime);
            Render();
            
            deltaTime = stopwatch.Elapsed.TotalSeconds;
            //Sets framerate
            if (deltaTime < frameTime)
            {
                int sleepTime = (int)(frameTime - deltaTime) * 1000;
                if (sleepTime > 0)
                    Thread.Sleep(sleepTime);
            }
        }
        Score.SaveHighScore();
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
        SpawnEnemies();
        foreach (Bullet bullet in player.bullets)
        {
            if (bullet != null)
            {
                bullet.UpdateBullet(deltaTime);
                
                foreach (Enemy enemy in enemies)
                {
                    if (enemy != null)
                    {
                        if (enemy.pos.X == bullet.X && enemy.pos.Y == bullet.Y)
                        {
                            enemy.TakeDamage();
                            bullet.isAlive = false;
                        }
                    }
                }
            }
        }
        
        foreach (Enemy enemy in enemies)
        {
            if(enemy != null)
            {
                enemy.Move(deltaTime);
                if ((int)enemy.pos.X == (int)player.pos.X && (int)enemy.pos.Y == (int)player.pos.Y)
                {
                    player.TakeDamage();
                    enemy.isAlive = false;

                }
            }
        }
        CheckDestruction();

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
        //foreach (Deathanimation deathanimation in deathanimations)
        //    if(deathanimation != null)
        //        deathanimation.DrawAnimation(deltaTime);
        Score.DisplayScore();
        player.DisplayPlayerHP();
    }
    void CheckDestruction()
    {
        player.HandleBulletDestruction();
        HandleEnemyDestruction();
        player.Destroy();
        //HandleAnimDestruction();
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.HandleBulletDestruction();
                enemy.Destroy();
                //if (enemy.isAlive == false && animationCounter < 5)
                //{
                //    deathanimations[animationCounter] = new(enemy.pos);
                //    animationCounter++;
                //    
                //}
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
                enemies[enemyCount] = new Enemy(2, random.Next(1, Console.WindowWidth), 1);
                enemyCount++;
            }
        }
    }

    //public void HandleAnimDestruction()
    //{
    //    for (int i = 0; i < animationCounter; i++)
    //    {
    //        if (!deathanimations[i].isAlive)  // Check if the bullet is no longer alive
    //        {
    //            // Shift the remaining animations in the array to fill the gap
    //            for (int j = i; j < animationCounter - 1; j++)
    //            {
    //                deathanimations[j] = deathanimations[j + 1];
    //            }
    //
    //            // Decrement the animation count
    //            animationCounter--;
    //
    //            // Set the last slot in the array to null (optional)
    //            deathanimations[animationCounter] = null;
    //        }
    //    }
    //}

    public void HandleEnemyDestruction()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (!enemies[i].isAlive)  // Check if the enemy is no longer alive
            {
                // Shift the remaining enemies in the array to fill the gap
                for (int j = i; j < enemyCount - 1; j++)
                {
                    enemies[j] = enemies[j + 1];
                }

                // Decrement the enemy count
                enemyCount--;
                Score.AddScore(100);
                // Set the last slot in the array to null (optional)
                enemies[enemyCount] = null;
            }
        }
    }
}
