using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

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

        const int MoveSpeed = 5;

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
            switch (dir)
            {
                case Directrion.Right:
                    if (x + MoveSpeed < world.Wall || x + MoveSpeed >= 0)
                        x += MoveSpeed;
                    break;
                case Directrion.Left:
                    if (x + MoveSpeed < world.Wall || x + MoveSpeed >= 0)
                        x -= MoveSpeed;
                    break;
            }
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
            //
            base.Update(dt, player, world);
        }
    }
}