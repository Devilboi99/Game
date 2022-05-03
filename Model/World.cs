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
    }
}