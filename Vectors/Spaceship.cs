﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


class Spaceship
{
    public Point pos = new();
    protected int shipHP = 2;
    public bool isAlive = true;
    double moveTimer = 1;
    double moveInterval = 1;

    public Spaceship(int shipHP, int x, int y)
    {
        this.shipHP = shipHP;
        pos.X = x;
        pos.Y = y;
    }
    public virtual void Draw()
    {
        Console.SetCursorPosition((int)pos.X, (int)pos.Y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("V");
        Console.ResetColor();
    }

    public void Destroy()
    {
        if (shipHP <= 0)
        {
            isAlive = false;
        }
    }

    public void TakeDamage()
    {
        shipHP -= 1;
    }
    public virtual void Move(double deltaTime)
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

