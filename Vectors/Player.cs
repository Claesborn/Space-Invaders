using System;




class Player : Spaceship
{
    //Point pos = new(60, 28);
    public Bullet[] bullets = new Bullet[15];
    int bulletCount = 0;
    int maxBullets = 5;
    public Player(int shipHP, int X, int Y) : base(shipHP, X, Y)
    {

    }

    public void Move(int x, int y)
    {
        pos.X += x;
        if (pos.X >= Console.WindowWidth - 1)
            pos.X = Console.WindowWidth - 1;
        if (pos.X <= 1)
            pos.X = 1;
        pos.Y += y;
        if (pos.Y >= Console.WindowHeight - 1)
            pos.Y = Console.WindowHeight - 1;
        if (pos.Y <= 1)
            pos.Y = 1;
    }

    public override void Draw()
    {
        Console.SetCursorPosition((int)pos.X, (int)pos.Y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("A");
        Console.ResetColor();
    }

    public void HandlePlayerInput() 
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.A)
            {
                Move(-1, 0);
            }
            if (keyInfo.Key == ConsoleKey.D)
            {
                Move(1, 0);
            }
            if (keyInfo.Key == ConsoleKey.S)
            {
                Move(0, 1);
            }
            if (keyInfo.Key == ConsoleKey.W)
            {
                Move(0, -1);
            }
            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                if(bulletCount < maxBullets)
                {
                    bullets[bulletCount] = new Bullet((int)pos.X, (int)pos.Y-1);
                    bulletCount++;
                }
            }

        }
    }
    public void HandleBulletDestruction()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!bullets[i].isAlive)  // Check if the bullet is no longer alive
            {
                // Shift the remaining bullets in the array to fill the gap
                for (int j = i; j < bulletCount - 1; j++)
                {
                    bullets[j] = bullets[j + 1];
                }

                // Decrement the bullet count
                bulletCount--;

                // Set the last slot in the array to null (optional)
                bullets[bulletCount] = null;
            }
        }
    }
}

