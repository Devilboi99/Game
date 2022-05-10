namespace Model
{
    public class World
    {
        public int Ground { get; }
        public int Wall { get; }

        public World(int ground, int wall)
        {
            Ground = ground;
            Wall = wall;
        }

        public bool InRoom(float rightBorder, float leftBorder)
        {
            return rightBorder < Wall && leftBorder >= 0;
        }

    }
}