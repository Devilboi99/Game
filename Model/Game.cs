using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Model
{
    public enum numlevel
    {
        First,
        Second,
        Third
    }

    public class Game
    {
        public static class configuration
        {
            public static void ConfigWithFiveSeconds(World currentWorld)
            {
                currentWorld.ChangeText("оооо нееет, дверь закрыта, можешь подождать секунд 5?");
                var thread = new Thread(() =>
                {
                    Thread.Sleep(5000);
                    currentWorld.door.UnLock();
                });
                thread.Start();
            }
            public static void ConfigWithLazyDev(World currentWorld)
            {
                currentWorld.ChangeText("Блин а можешь доделать игру?");
                var thread = new Thread(() =>
                {
                    Thread.Sleep(3000);
                    currentWorld.ChangeText("А то чет получается концвки нету((");
                    Thread.Sleep(3000);
                    currentWorld.ChangeText("Там эта... нужно дверь с открыть плз");
                });
                thread.Start();
            }
        }

        Dictionary<Enum, World> levels = new Dictionary<Enum, World>();
        public Dictionary<Enum, Action> ActionWithCloseDoor = new Dictionary<Enum, Action>();
        public numlevel CurrentLevelNumber { get; private set; }


        public Game(int height, int width)
        {
            CreateLevels(height, width);
            CurrentLevelNumber = numlevel.First;
        }

        public World NextLevel => levels[++CurrentLevelNumber];


        public void CreateLevels(int ground, int rightSide)
        {
            levels[numlevel.First] = new World(ground, rightSide, "Где же выход?", "10");
            ActionWithCloseDoor[numlevel.First] = () => throw new Exception();
            
            levels[numlevel.Second] = new World(ground, rightSide, "Ой какое милое", "01");
            ActionWithCloseDoor[numlevel.Second] = () => configuration.ConfigWithFiveSeconds(levels[numlevel.Second]);
            
            levels[numlevel.Third] = new World(ground, rightSide, "ааа блин этот левел не сделан", "00");
            ActionWithCloseDoor[numlevel.Third] = () => configuration.ConfigWithLazyDev(levels[numlevel.Third]);
        }

        public World this[numlevel level]
            => levels[level];
    }
}