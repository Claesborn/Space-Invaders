using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Enemy : Spaceship
{
    public Bullet[] bullets = new Bullet[2];
    int bulletCount = 0;
    double moveTimer = 1;
    double moveInterval = 1;
    public Enemy(int shipHP, int X, int Y) : base(shipHP, X, Y)
    {

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

    public void Move(double deltaTime)
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

