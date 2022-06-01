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
        public numlevel CurrentLevelNunber { get; set; }

        public Game()
            => CurrentLevelNunber = numlevel.First;

        public void CreateLevels(int ground, int rightSide)
        {
           
            levels[numlevel.First] = new World(ground, rightSide, "Где же выход?");
            levels[numlevel.second] = new World(ground, rightSide, "пошел нафиг");
        }

        public World this[numlevel level]
            => levels[level];
    }
}