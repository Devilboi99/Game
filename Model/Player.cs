using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using Timer = System.Timers.Timer;

namespace Model
{
    public enum Directrion
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Player : Physics
    {
        public float Width { get; set; }
        public float Height { get; set; }


        const float MoveStep = 5;

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
            Width = 30;
            Height = 30;
            Physics physics = new Physics();
        }

        public void Move(Directrion dir, World world)
        {
            if (Velocity.X > 30 || Velocity.X < -30) return;

            switch (dir)
            {
                case Directrion.Right:
                    x += MoveStep;
                    Velocity = new PointF(Velocity.X + MoveStep, Velocity.Y);
                    break;
                case Directrion.Left:
                    x -= MoveStep;
                    Velocity = new PointF(Velocity.X - MoveStep, Velocity.Y);
                    break;
            }
            
        }

        public void Resistance(float resist)
        {
            if (Velocity.X > 0)
                Velocity = new PointF(Velocity.X - resist, Velocity.Y);
            if (Velocity.X < 0)
                Velocity = new PointF(Velocity.X + resist, Velocity.Y);
        }

        public void jump(Player player, World world)
        {
            const int jumpSpeed = 50;

            if (y + Height > world.Ground - 10) //только если стоим на земле
                Velocity = new PointF(player.Velocity.X, player.Velocity.Y - jumpSpeed); //прыгаем вверх
        }

        public override void Update(float dt, Player player, World world)
        {
            //столкновенине с землей?
            if (y + Height > world.Ground)
                OnGroundCollision(world.Ground - Height);
            if (Math.Abs(y + Height - world.Ground) < 1e-00)
                Resistance(0.1f);
            Resistance(00000.1f);
            if (x + Width > world.Wall)
            {
                x = world.Wall - Width;
            }
            if (x < 0)
                x = 0;
            //
            base.Update(dt, player, world);
        }
    }
}