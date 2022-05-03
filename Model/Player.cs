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
        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
            Physics physics = new Physics();
        }
    
        public void Move(Directrion dir,Player player)
        {
            switch (dir)
            {
                
            }
        }

        public void jump(Player player,World world)
        {
            const int jumpSpeed = 50;

            if (y + Height / 2 > world.ground - 10)//только если стоим на земле
                Velocity = new PointF(player.Velocity.X, player.Velocity.Y - jumpSpeed);//прыгаем вверх
        }
        public override void Update(float dt,Player player,World world)
        {
            //столкновенине с землей?
            if (y + Height / 2 > world.ground)
                OnGroundCollision(world.ground - Height / 2);
            //
            base.Update(dt,player,world);
        }
        
    }
}