using System;
using Model;
using NUnit.Framework;


namespace TestModel
{
    public class Tests
    {

        private Game game = new Game(440, 300);
        

        [SetUp]
        public void Setup()
        {
        }
        
        
        [Test]
        public void CheckWorkSwitchWorld()
        {
           var currentWorld = game[game.CurrentLevelNumber];
           Assert.True(currentWorld.Door.IsOpen);
           currentWorld = game.NextLevel;
           Assert.False(currentWorld.Door.IsOpen);
           var player = new Player( (int) currentWorld.Door.X, (int)currentWorld.Door.Y);
           var text = "";
           currentWorld.PlayerInDoor(player, () =>
           {
               text = "doogs";
               currentWorld.Door.UnLock();
           });
           Assert.True(currentWorld.Door.IsOpen);
           Assert.AreEqual("doogs", text);
        }

        [Test]
        public void WorkDoor()
        {
        }
    }
}