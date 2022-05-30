using System.Drawing;

namespace Model
{
    public enum Directrion
    {
        Left,
        Right
    }

    public class Player : Physics
    {
        public float Width { get;}
        public float Height { get;}

        private const float SpeedStep = 5;

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
            Width = 50;
            Height = 80;
        }

        public void Move(Directrion dir)
        {
            if (Velocity.X > 30 || Velocity.X < -30) return;

            switch (dir)
            {
                case Directrion.Right:
                    x += SpeedStep;
                    Velocity = new PointF(Velocity.X + SpeedStep,
                        Velocity.Y); // Замутитить Moving and stopMoving для передвжиение в полёте.
                    break;
                case Directrion.Left:
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
            player.InRoom(world);
            world.PlayerInDoor(player);
            if (world.IsFindExist)
                player.x = 30;
            base.Update(dt, player, world);
        }

        private void InRoom(World world)
        {
            if (x + Width > world.RightSide)
                x = world.RightSide - Width;
            if (x < 0)
                x = 0;
        }
    }
}