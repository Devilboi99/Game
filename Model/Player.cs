using System;
using System.Drawing;

namespace Model
{
    public enum Direction
    {
        Left,
        Right
    }

    public class Player : Physics, ICreature
    {
        public float Width { get; }
        public float Height { get; }

        private const float SpeedStep = 2;
        private const int RespawnPositionX = 30;
        

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Width = 10;
            Height = 23;
        }

        public void Move(Direction dir)
        {
            if (Math.Abs(Velocity.X) > 30) return;

            switch (dir)
            {
                case Direction.Right:
                    X += SpeedStep;
                    Velocity = new PointF(Velocity.X + SpeedStep,
                        Velocity.Y);
                    break;
                case Direction.Left:
                    X -= SpeedStep;
                    Velocity = new PointF(Velocity.X - SpeedStep, Velocity.Y);
                    break;
            }
        }

        private void Resistance(float resist)
        {
            if (Velocity.X > 0)
                Velocity = new PointF(Velocity.X - resist, Velocity.Y);
            if (Velocity.X < 0)
                Velocity = new PointF(Velocity.X + resist, Velocity.Y);
        }

        public void Jump(World world)
        {
            const int jumpSpeed = 50;
            if (Y + Height > world.Ground - 10 || world.IsOnFloor(this))
                Velocity = new PointF(Velocity.X, Velocity.Y - jumpSpeed);
        }

        public override void Update(float dt, Player player, World world)
        {
            if (Y + Height > world.Ground)
            {
                OnGroundCollision(world.Ground - Height);
                Resistance(0.50f);
            }
            
            Resistance(0.10f);

            world.IsOnFloor(player);
            world.InRoom(player);
            if (world.IsCompleted)
                X = RespawnPositionX;
            base.Update(dt, player, world);
        }

        public void ChangePositionPlayerX(float newPosition)
            => X = newPosition;
    }
}