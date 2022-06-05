using System;
using System.Collections.Generic;
using System.Threading;


namespace Model
{
    public enum NumLevel
    {
        First,
        Second,
        Third
    }

    public class Game
    {
        private static class Сonfiguration
        {

            public static void JokeFromDev(World currentLevel)
            {
                currentLevel.ChangeText("Ты думал, что дверь - выход?");
                var thread = new Thread(() =>
                {
                    Thread.Sleep(4000);
                    currentLevel.ChangeText("мдэээ выход сверху слева в стене на тоооом блоке");
                    Thread.Sleep(10000);
                    currentLevel.ChangeText("Да я прикалываюсь. конечно, дверь - выход. иди обратно");
                    currentLevel.door.UnLock();
                });
                thread.Start();
            }
            
            public static void ConfigWithFiveSeconds(World currentLevel)
            {
                currentLevel.ChangeText("оооо нееет, дверь закрыта, можешь подождать секунд 5?");
                var thread = new Thread(() =>
                {
                    Thread.Sleep(5000);
                    currentLevel.door.UnLock();
                });
                thread.Start();
            }

            public static void ConfigWithLazyDev(World currentLevel)
            {
                currentLevel.ChangeText("Блин а можешь доделать игру?");
                var thread = new Thread(() =>
                {
                    Thread.Sleep(3000);
                    currentLevel.ChangeText("А то чет получается концвки нету((");
                    Thread.Sleep(3000);
                    currentLevel.ChangeText("Там эта... нужно дверь открыть плз");
                    Thread.Sleep(8000);
                    currentLevel.ChangeText("Ну давай не ленись там концова крутая, честно");
                    Thread.Sleep(8000);
                    currentLevel.ChangeText("ты еще здесь? может и фон найдешь?");
                });
                thread.Start();
            }
        }

        Dictionary<Enum, World> levels = new Dictionary<Enum, World>();   // продумать защиту этих штук
        public Dictionary<Enum, Action> ActionWithDoor = new Dictionary<Enum, Action>();
        public NumLevel CurrentLevelNumber { get; private set; }
        
        public Game(int height, int width)
        {
            CreateLevels(height, width);
            CurrentLevelNumber = NumLevel.First;
        }

        public World NextLevel => levels[++CurrentLevelNumber];

        private void CreateLevels(int ground, int rightSide)
        {
            levels[NumLevel.First] = new World(ground, rightSide, "Где же выход?", "00");
            ActionWithDoor[NumLevel.First] = () => Сonfiguration.JokeFromDev(levels[NumLevel.First]);

            levels[NumLevel.Second] = new World(ground, rightSide, "Ой какое милое", "01");
            ActionWithDoor[NumLevel.Second] = () =>  Сonfiguration.ConfigWithFiveSeconds(levels[NumLevel.Second]);

            levels[NumLevel.Third] = new World(ground, rightSide, "ааа блин этот Уровень не доделан", "00");
            ActionWithDoor[NumLevel.Third] = () => Сonfiguration.ConfigWithLazyDev(levels[NumLevel.Third]);
        }

        public World this[NumLevel level]
            => levels[level];
    }
}