using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Bullet
{
    public int X = 0;
    public int Y = 0;
    //Point pos;
    double speed = 0.2;
    double tick = 0;
    double lifeTime = 8;
    public bool isAlive = true;
    public Bullet(int x, int y)
    {
        X = x;
        Y = y;
        //pos = new(x, y);
    }

    public void UpdateBullet(double deltaTime)
    {
        tick += deltaTime;
        while(tick >= speed)
        {
            //pos.Move(0, -1);
            Y -= 1;
            tick -= speed;
        }
        lifeTime -= deltaTime;
        if(lifeTime < 0 || Y < 2)
        {
            isAlive = false;
        }
    }
    public void Draw()
    {
        if(isAlive)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("*");
        }
        
    }
}

