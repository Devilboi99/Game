using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Model
{
    public enum numlevel
    {
        First,
        second,
    }

    public class Game
    {
        Dictionary<Enum, World> levels = new Dictionary<Enum, World>();
        public numlevel CurrentLevelNumber { get; private set; }
        private string configuration;

        public Game(int height, int width)
        {
            CreateLevels(height, width);
            CurrentLevelNumber = numlevel.First;
        }

        public World NextLevel => levels[++CurrentLevelNumber];

        public void CreateLevels(int ground, int rightSide)
        {
            levels[numlevel.First] = new World(ground, rightSide, "Где же выход?", "10");
            levels[numlevel.second] = new World(ground, rightSide, "Ой какое милое", "01");
        }

        public World this[numlevel level]
            => levels[level];
    }
}