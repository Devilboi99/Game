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
           Assert.False(currentWorld.Door.IsOpen);
           currentWorld = game.NextLevel;
           Assert.True(currentWorld.Monster.IsLive);
        }

        [Test]
        public void WorkDoorAction()
        {
            var currentWorld = game[game.CurrentLevelNumber];
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
        
        
    }
}