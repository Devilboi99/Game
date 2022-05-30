using System;
using Model;
using NUnit.Framework;

namespace TestModel
{
    public class Tests
    {
        private float startPositionX = 10;
        private Player player = new Player(10, 10);
        private World world = new World(500, 300);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckMoveRightSide()
        {
            player.Move(Directrion.Left);
        }

        [Test]
        public void WorkDoor()
        {
            world.CreateObjectWorld();
            while (world.IsFindExist!)
                player.Move(Directrion.Right);
            Assert.AreEqual(startPositionX, player.x);
        }
    }
}