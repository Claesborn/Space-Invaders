
using System.Diagnostics;


class Game
{
    static Random random = new();
    Player player;
    Stopwatch stopwatch = new();
    Spaceship[] enemies = new Spaceship[50];
    double deltaTime = 0;
    int maxEnemies = 20;
    int enemyCount = 0;
    double tick = 3;
    double spawnTimer = 3;
    double frameTime = 1.0 / 25;
    //Deathanimation[] deathanimations = new Deathanimation[20];
    //int animationCounter = 0;
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
                
                foreach (Spaceship enemy in enemies)
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
        
        foreach (Spaceship enemy in enemies)
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
        foreach (Spaceship enemy in enemies)
        {
            if (enemy != null)
            {
                //foreach (Bullet bullet in enemy.bullets)
                //{
                //    if (bullet != null)
                //        bullet.Draw();
                //}
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
        foreach (Spaceship enemy in enemies)
        {
            if (enemy != null)
            {
                //enemy.HandleBulletDestruction();
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

        if (Score.score > 1000 && random.Next(0, 11) > 8 && tick <= 0 && enemyCount < maxEnemies)
        {
            tick += spawnTimer;
            enemies[enemyCount] = new Boss(5, random.Next(1, Console.WindowWidth), 1);
            enemyCount++;
        }
        else if (enemyCount < maxEnemies && tick <= 0)
        {
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

                if (enemies[enemyCount].GetType() == typeof(Enemy))
                    Score.AddScore(100);
                else if (enemies[enemyCount].GetType() == typeof(Boss))
                    Score.AddScore(300);
                // Set the last slot in the array to null (optional)
                enemies[enemyCount] = null;
            }
        }
    }
}
