using System;
using System.Collections.Generic;
using System.Linq;


namespace Model
{
    public class World
    {
        private class Floor : IObjectWorld
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

        public class Door : IObjectWorld
        {
            public bool isOpen { get; private set; }
            public float x { get; }
            public float y { get; }
            public float x2 { get; }
            public float y2 { get; }

            public void UnLock() => isOpen = true;

            public Door(float x, float y, float x2, float y2, bool isOpen)
            {
                this.x = x;
                this.y = y;
                this.x2 = x2;
                this.y2 = y2;
                this.isOpen = isOpen;
            }
        }

        public void ChangeText(string newText)
            => TextLevel = newText;

        public string TextLevel { get; private set; }
        private List<Floor> floors;

        public Door door;
        public bool IsCompleted { get; private set; }
        public int Ground { get; }
        public int RightSide { get; }

        public Monster Monster { get; private set; }

        public World(int rightSide, int ground, string textLevel, string configuration)
        {
            Ground = rightSide;
            RightSide = ground;
            floors = new List<Floor>();
            TextLevel = textLevel;
            CreateObjectWorld(configuration);
        }

        private void CreateObjectWorld(string configuration)
        {
            var config = new Queue<bool>(SetConfig(configuration));
            floors.Add(new Floor(RightSide / 2 - 150, Ground - 130, RightSide / 2 + 150, Ground - 120));
            floors.Add(new Floor(RightSide / 2 - 250, Ground - 50, RightSide / 2 + 150, Ground - 40));
            floors.Add(new Floor(RightSide / 2 + 150, Ground - 50, RightSide / 2 + 250, Ground - 40));
            floors.Add(new Floor(0, Ground / 1.6f, RightSide / 4f, Ground / 1.6f + 10));
            floors.Add(new Floor(RightSide - RightSide / 4, Ground / 1.6f, RightSide, Ground / 1.6f + 10));
            door = new Door(RightSide - 20, Ground - 90, RightSide, Ground, config.Dequeue()); 
            Monster = new Monster(RightSide / 2, Ground / 2, config.Dequeue());
        }

        private IEnumerable<bool> SetConfig(string config)
            => config.ToBool().ToList();


        private static bool Overlaps(Player player, IObjectWorld obj)
        {
            return player.x + player.Width >= obj.x &&
                   obj.x2 >= player.x &&
                   player.y <= obj.y2 &&
                   player.y + player.Height >= obj.y;
        }

        public void PlayerInDoor(Player player, Action action)
        {
            if (!door.isOpen && Overlaps(player, door))
                action();

            if (Overlaps(player, door) && door.isOpen)
                IsCompleted = true;
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

        public bool IsLevelCompleted()
            => IsCompleted && door.isOpen;

        public void InRoom(Player player)
        {
            if (player.x + player.Width > RightSide)
                player.ChangePositionPlayerX(RightSide - player.Width);
            if (player.x < 0)
                player.ChangePositionPlayerX(0);
        }
    }

    public static class BoolExtension
    {
        public static IEnumerable<bool> ToBool(this string line)
        {
            foreach (var num in line)
            {
                if (num == '1') yield return true;
                else yield return false;
            }
        }
    }
}