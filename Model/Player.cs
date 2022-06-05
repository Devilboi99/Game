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

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
            Width = 50;
            Height = 80;
        }

        public void Move(Direction dir)
        {
            if (Velocity.X > 20 || Velocity.X < -20) return;

            switch (dir)
            {
                case Direction.Right:
                    x += SpeedStep;
                    Velocity = new PointF(Velocity.X + SpeedStep,
                        Velocity.Y); // Замутитить Moving and stopMoving для передвжиение в полёте.
                    break;
                case Direction.Left:
                    x -= SpeedStep;
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

            if (y + Height > world.Ground - 10 || world.IsOnFloor(this)) //только если стоим на земле или на блоке
                Velocity = new PointF(Velocity.X, Velocity.Y - jumpSpeed); //прыгаем вверх
        }

        public override void Update(float dt, Player player, World world)
        {
            if (y + Height > world.Ground)
            {
                OnGroundCollision(world.Ground - Height);
                Resistance(0.50f);
            }

            Resistance(0.10f);
            world.IsOnFloor(player);
            world.InRoom(player);
            if (world.IsCompleted)
                x = 33;
            base.Update(dt, player, world);
        }

        public void ChangePositionPlayerX(float newPosition)
            => x = newPosition;
    }
}