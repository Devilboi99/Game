using System.Collections;
using System.Dynamic;
using System.Runtime.Intrinsics.X86;

namespace Model
{
    public class Monster : Physics, ICreature
    {
        public float Width { get; }
        public float Height { get; }

        public bool Islive { get; }

        public Monster(int x, int y)
        {
            this.x = x;
            this.y = y;
            Islive = true;
            Width = 50;
            Height = 80;
        }
        
        public void GoTo(Player player)
        {
            if (x > player.x)
                x -= 1;
            if (x < player.x)
                x += 1;
            if (y > player.y)
                y -= 1;
            if (y < player.y)
                y += 1;
        }

        public override void Update(float dt, Player player, World world)
        {
            GoTo(player);   
        }
    }
}