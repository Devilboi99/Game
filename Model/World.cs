using System;
using System.Collections.Generic;

namespace Model
{
    public class World
    {
        private class Floor
        {
            public float x { get; }
            public float y { get; }
            public float x2 { get; }
            public float y2 { get; }

            public Floor(float x, float y, float x2, float y2)
            {
                this.x = x;
                this.y = y;
                this.x2 = x2;
                this.y2 = y2;
            }
        }

        private List<Floor> floors;
        public int Ground { get; }
        public int Wall { get; }

        public World(int ground, int wall)
        {
            Ground = ground;
            Wall = wall;
            floors = new List<Floor>();
        }

        public void CreateObjectWorld()
        {
            floors.Add(new Floor(Wall / 2 - 150, Ground - 100, Wall / 2 + 150, Ground));
        }

        public bool Overlaps(Player player, int indexFloor)
        {
            return player.x + player.Width >= floors[indexFloor].x &&
                   floors[indexFloor].x2 >= player.x &&
                   player.y <= floors[indexFloor].y2 &&
                   player.y + player.Height >= floors[indexFloor].y;
        }

        public void CheckFloor(Player player)
        {
            for (var j = 0; j < floors.Count; j++)
                if (Overlaps(player, j))
                    player.OnGroundCollision(floors[j].y - player.Height);
        }
    }
}