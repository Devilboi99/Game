using System;
using System.Collections.Generic;


public enum Directrion
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Player
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Player(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public static void  Move(Directrion dir)
        {
            Func<(int, int)> move;
            var command = new Dictionary<Directrion, Func<(int, int)>>();
            command.Add(Directrion.Down, () => );
            
        }
    }