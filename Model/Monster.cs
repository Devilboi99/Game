namespace Model
{
    public class Monster : Physics, ICreature
    {
        public float Width { get; }
        public float Height { get; }

        private const int Speed = 2;

        public bool IsLive { get; }

        public Monster(int x, int y, bool isLive = false)
        {
            X = x;
            Y = y;
            IsLive = isLive;
            Width = 50;
            Height = 50;
        }

        public void GoTo(Player player)
        {
            if (X > player.X)
                X -= Speed;
            if (X < player.X)
                X += Speed;
            if (Y > player.Y)
                Y -= Speed;
            if (Y < player.Y)
                Y += Speed;
        }

        public override void Update(float dt, Player player, World world)
        {
            GoTo(player);
        }
    }
}