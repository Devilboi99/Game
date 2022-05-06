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

        public bool Moving { get; set; }

        const float MoveSpeed = 5;

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
            Moving = true;
            switch (dir)
            {
                case Directrion.Right:
                    Velocity = new PointF(Velocity.X + MoveSpeed, Velocity.Y);
                    if (x + MoveSpeed < world.Wall || x + MoveSpeed >= 0)
                        x += MoveSpeed;
                    break;
                case Directrion.Left:
                    Velocity = new PointF(Velocity.X - MoveSpeed, Velocity.Y);
                    if (x + MoveSpeed < world.Wall || x + MoveSpeed >= 0)
                        x -= MoveSpeed;
                    break;
            }
        }

        public void Resistance()
        {
            if (Velocity.X > 0)
                Velocity = new PointF(Velocity.X - 00000.1f, Velocity.Y);
            if (Velocity.X < 0)
                Velocity = new PointF(Velocity.X + 00000.1f, Velocity.Y);
            if (Velocity.X == 0)
                Moving = false;
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
            if (Math.Abs(y + Height - world.Ground) < 2e-98 && Moving)
                Velocity = new PointF(0, Velocity.Y);
            Resistance();
            //
            base.Update(dt, player, world);
        }
    }
}