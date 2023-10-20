using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



internal class Deathanimation
{
    Point pos;
    double timer = 0.1;
    double timer2 = 0.1;
    double animationTime = 0.1;
    public bool isAlive = true;
    public Deathanimation(Point pos)
    {
        pos = new(pos);
    }

    public void DrawAnimation(double deltaTime)
    {
        while (isAlive)
        {
            Console.SetCursorPosition((int)pos.X, (int)pos.Y);
            if(timer > 0)
                Console.Write("*");
            else
            {
                Console.Write("X");
                timer2 -= deltaTime;
                if (timer2 <= 0)
                    isAlive = false;
            }
            timer -= deltaTime;
            
        }
    }
}


