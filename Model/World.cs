using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class World
    {
        public string TextLevel { get; set; }
        private readonly List<IObjectWorld> _floors;

        public Door Door;
        public bool IsCompleted { get; private set; }
        public int Ground { get; }
        public int RightSide { get; }

        private bool _isFirst = true;

        public Monster Monster { get; private set; }

        public World(int rightSide, int ground, string textLevel, string configuration)
        {
            Ground = rightSide;
            RightSide = ground;
            _floors = new List<IObjectWorld>();
            TextLevel = textLevel;
            CreateObjectWorld(configuration);
        }

        private void CreateObjectWorld(string configuration)
        {
            var config = new Queue<bool>(SetConfig(configuration));
            _floors.Add(new Floor(RightSide / 2 - 150, Ground - 130, RightSide / 2 + 150, Ground - 120));
            _floors.Add(new Floor(0, Ground / 1.6f, RightSide / 4f, Ground / 1.6f + 10));
            _floors.Add(new Floor(RightSide - RightSide / 4, Ground / 1.6f, RightSide, Ground / 1.6f + 10));
            Door = new Door(RightSide - 20, Ground - 90, RightSide, Ground, config.Dequeue());
            Monster = new Monster(RightSide - 10, Ground / 2, config.Dequeue());
        }

        private IEnumerable<bool> SetConfig(string config)
            => config.ToBool().ToList();


        private static bool Overlaps(Player player, IObjectWorld obj)
        {
            return player.X + player.Width >= obj.X &&
                   obj.X2 >= player.X &&
                   player.Y <= obj.Y2 &&
                   player.Y + player.Height >= obj.Y;
        }

        public static bool MonsterInPlayer(Player player, Monster obj)
        {
            return player.X + player.Width >= obj.X &&
                   obj.X + obj.Width >= player.X &&
                   player.Y <= obj.Y + obj.Height &&
                   player.Y + player.Height >= obj.Y;
        }

        public void PlayerInDoor(Player player, Action action)
        {
            if (!Door.IsOpen && Overlaps(player, Door) && _isFirst)
            {
                action();
                _isFirst = false;
            }
                
            
            if (Overlaps(player, Door) && Door.IsOpen)
                IsCompleted = true;
        }

        public bool IsOnFloor(Player player)
        {
            foreach (var floor in _floors)
            {
                if (!Overlaps(player, floor)) continue;
                
                player.OnGroundCollision(floor.Y - player.Height);
                return true;
            }

            return false;
        }

        public bool IsLevelCompleted()
            => IsCompleted && Door.IsOpen;

        public void InRoom(Player player)
        {
            if (player.X + player.Width > RightSide)
                player.ChangePositionPlayerX(RightSide - player.Width);
            if (player.X < 0)
                player.ChangePositionPlayerX(0);
        }
    }

    public class Door : IObjectWorld
    {
        public bool IsOpen { get; private set; }
        public float X { get; }
        public float Y { get; }
        public float X2 { get; }
        public float Y2 { get; }

        public void UnLock() => IsOpen = true;

        public Door(float x, float y, float x2, float y2, bool isOpen)
        {
            X = x;
            Y = y;
            X2 = x2;
            Y2 = y2;
            IsOpen = isOpen;
        }
    }

    public class Floor : IObjectWorld
    {
        public float X { get; }
        public float Y { get; }
        public float X2 { get; }
        public float Y2 { get; }


        public Floor(float x, float y, float x2, float y2)
        {
            this.X = x;
            this.Y = y;
            this.X2 = x2;
            this.Y2 = y2;
        }
    }

    public static class BoolExtension
    {
        public static IEnumerable<bool> ToBool(this string line)
            => line.Select(x => x == '1');
    }
}