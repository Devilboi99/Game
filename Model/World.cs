using System.Collections.Generic;



namespace Model
{
    public class World
    {
        private class Floor : IObjectWorld
        {
            public override float x { get; }
            public override float y { get; }
            public override float x2 { get; }
            public override float y2 { get; }

            public Floor(float x, float y, float x2, float y2)
            {
                this.x = x;
                this.y = y;
                this.x2 = x2;
                this.y2 = y2;
            }
        }

        private class Door : IObjectWorld
        {
            public override float x { get; }
            public override float y { get; }
            public override float x2 { get; }
            public override float y2 { get; }


            public Door(float x, float y, float x2, float y2)
            {
                this.x = x;
                this.y = y;
                this.x2 = x2;
                this.y2 = y2;
            }
        }


        private List<Floor> floors;

        private Door door;
        public bool IsFindExist { get; private set; }
        public int Ground { get; }
        public int RightSide { get; }

        public World(int ground, int rightSide)
        {
            Ground = ground;
            RightSide = rightSide;
            floors = new List<Floor>();
        }

        public void CreateObjectWorld()
        {
            floors.Add(new Floor(RightSide / 2 - 150, Ground - 130, RightSide / 2 + 150, Ground - 120));
            floors.Add(new Floor(RightSide / 2 - 250, Ground - 50, RightSide / 2 + 150, Ground - 40));
            floors.Add(new Floor(RightSide / 2 + 150, Ground - 50, RightSide / 2 + 250, Ground - 40));
            floors.Add(new Floor(0, Ground / 1.6f, RightSide / 4f, Ground / 1.6f + 10));
            floors.Add(new Floor(RightSide - RightSide / 4, Ground / 1.6f, RightSide, Ground / 1.6f + 10));
            door = new Door(RightSide - 20, Ground - 90, RightSide, Ground);
        }

        private static bool Overlaps(Player player, IObjectWorld obj)
        {
            return player.x + player.Width >= obj.x &&
                   obj.x2 >= player.x &&
                   player.y <= obj.y2 &&
                   player.y + player.Height >= obj.y;
        }

        public void PlayerInDoor(Player player)
        {
            if (Overlaps(player, door))
                IsFindExist = true;
        }

        public bool IsOnFloor(Player player)
        {
            foreach (var floor in floors)
            {
                if (Overlaps(player, floor))
                {
                    player.OnGroundCollision(floor.y - player.Height);
                    return true;
                }
            }

            return false;
        }
    }
}