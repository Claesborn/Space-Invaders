using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class Boss : Spaceship
{
    public Bullet[] bullets = new Bullet[4];
    int bulletCount = 0;
    double moveTimer = 0.8;
    double moveInterval = 0.8;
    public Boss(int shipHP, int x, int y) : base(shipHP, x, y)
    {

    }

    public override void Draw()
    {
        Console.SetCursorPosition((int)pos.X, (int)pos.Y);
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("W");
        Console.ResetColor();
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
    public override void Move(double deltaTime)
    {
        moveTimer -= deltaTime;
        if (moveTimer <= 0)
        {
            pos.Y++;
            if (pos.Y > Console.WindowHeight - 1)
                pos.Y = 1;
            moveTimer += moveInterval;

        }

    }

}
