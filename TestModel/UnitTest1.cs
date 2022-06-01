using System;
using Model;
using NUnit.Framework;


namespace TestModel
{
    public class Tests
    {
        private float startPositionX = 10;
        private Player player = new Player(10, 10);
        private World world = new World(500, 300, "где же выход?");

        private DateTime lastUpdate = DateTime.MinValue;

        new void Update()
        {
            var now = DateTime.Now;
            var dt = (float) (now - lastUpdate).TotalMilliseconds / 100f;

            if (lastUpdate != DateTime.MinValue)
                player.Update(dt, player, world);

            lastUpdate = now;
        }

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
            Assert.AreEqual("где же выход?", world.TextLevel);
            do
            {
                Update();
                player.Move(Directrion.Right);
            } while (!world.IsCompleted);
           
               

            Assert.AreEqual(true, world.IsCompleted);
            Assert.AreEqual("Пошел Нафиг", world.TextLevel);
        }
    }
}