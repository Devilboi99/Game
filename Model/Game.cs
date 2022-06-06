using System;
using System.Collections.Generic;
using System.Threading;

namespace Model
{
    public enum NumLevel
    {
        First,
        Second,
        Third,
        Fourth

    }

    public class Game
    {
        private static class Сonfiguration
        {
            public static void Config(World level, string startText, (int, string)[] replicas, Action end)
            {
                level.TextLevel = startText;
                var thread = new Thread(() =>
                {
                    foreach (var (seconds, text) in replicas)
                    {
                        Thread.Sleep(seconds * 1000);
                        if (text != null)
                            level.TextLevel = text;
                    }

                    end();
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

        public World NextLevel => levels.TryGetValue(++CurrentLevelNumber, out var level) ? level : null;

        private void CreateLevels(int ground, int rightSide) //  Оно здесь!! можешь тесты глянуть они помогут.
        {
            levels[NumLevel.First] = new World(ground, rightSide, "Где же выход?", "00");
            ActionWithDoor[NumLevel.First] = () => Сonfiguration.Config(
                levels[NumLevel.First],
                "Ты думал, что дверь - выход?",
                JokeFromDevReplicas,
                () => levels[NumLevel.First].Door.UnLock());

            levels[NumLevel.Second] = new World(ground, rightSide, "Ой какое милое", "01");
            ActionWithDoor[NumLevel.Second] = () => Сonfiguration.Config(
                levels[NumLevel.Second],
                "оооо нееет, дверь закрыта, можешь подождать секунд 5?",
                FiveSecondsReplicas,
                () => levels[NumLevel.Second].Door.UnLock());

            levels[NumLevel.Third] = new World(ground, rightSide, "ааа блин этот Уровень не доделан", "00");
            ActionWithDoor[NumLevel.Third] = () => Сonfiguration.Config(
                levels[NumLevel.Third],
                "а можешь дописать игру?",
                LazyDevTexts,
                () => {});
            
            levels[NumLevel.Fourth] = new World(ground, rightSide, "Ура ура конец игры, ты рад?", "10");
            ActionWithDoor[NumLevel.Fourth] = () => Сonfiguration.Config(
                levels[NumLevel.Fourth],
                "а можешь дописать игру?",
                LazyDevTexts,
                () => {});
        }

        public World this[NumLevel level]
            => levels[level];

        private static readonly (int, string)[] JokeFromDevReplicas = {
            (4, "мдэээ выход сверху слева в стене на тоооом блоке"),
            (10, "Да я прикалываюсь. конечно, дверь - выход. иди обратно")
        };
        
        private static readonly (int, string)[] FiveSecondsReplicas = {
            (5, null)
        };
        
        private static readonly (int, string)[] LazyDevTexts = {
            (3, "А то чет получается концовки нету(("),
            (3, "Там эта... нужно дверь открыть плз"),
            (8, "Ну давай не ленись там концова крутая, честно"),
            (8, "ты еще здесь? может и фон найдешь?")
        };
    }
}