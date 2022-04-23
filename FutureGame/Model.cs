using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


public enum Directrion
{
    Up,
    Down,
    Left,
    Right
}

public class Player
{
    public static int x { get; private set; }
    public static int y { get; private set; }

    public Player(int x, int y)
    {
        Player.x = x;
        Player.y = y;
    }

    public static void Move(Directrion dir)
    {
        switch (dir)
        {
            case Directrion.Down:
                y += 1;
                break;
            case Directrion.Up:
                y -= 1;
                break;
            case Directrion.Left:
                x -= 1;
                break;
            case Directrion.Right:
                x += 1;
                break;
        }
    }

    public static void fall()
    {
        y += 1;
    }
}

class World
{
    public int ground { get; }

    public World(int ground)
    {
        this.ground = ground;
    }
}
//var command = new Dictionary<Directrion, Func<int>>();
//command.Add(Directrion.Down, () => 1);
//command.Add(Directrion.Left, () => -1);
//command.Add(Directrion.Right, () => 1);
//command.Add(Directrion.Down, () => -1);