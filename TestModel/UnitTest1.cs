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
           Assert.True(currentWorld.door.isOpen);
           currentWorld = game.NextLevel;
           Assert.False(currentWorld.door.isOpen);
           var player = new Player( (int) currentWorld.door.x, (int)currentWorld.door.y);
           var text = "";
           currentWorld.PlayerInDoor(player, () =>
           {
               text = "doogs";
               currentWorld.door.UnLock();
           });
           Assert.True(currentWorld.door.isOpen);
           Assert.AreEqual("doogs", text);
        }

        [Test]
        public void WorkDoor()
        {
        }
    }
}