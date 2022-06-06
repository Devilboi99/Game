using System.Drawing;

namespace Model
{
    public class Physics
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }

        public PointF Velocity { get; set; }

        public float Mass { get; set; }

        public float Spring { get; set; }

        public bool Gravity { get; set; }

        public PointF Force { get; set; }


        public Physics()
        {
            Spring = 0.3f;
            Mass = 2f;
            Gravity = true;
        }
        

        public virtual void Update(float dt, Player player, World world)
        {
            var force = Force;
            Force = Point.Empty;
            
            if (Gravity)
                force = new PointF(force.X, force.Y + 9.8f * Mass);
            
            var ax = force.X / Mass;
            var ay = force.Y / Mass;
            
            Velocity = new PointF(Velocity.X + ax * dt, Velocity.Y + ay * dt);
            
            X += Velocity.X * dt;
            Y += Velocity.Y * dt;
        }

        public void OnGroundCollision(float groundY)
        {
            if (Velocity.Y < -float.Epsilon) return;
            Y = groundY - 0.0001f;
            Velocity = new PointF(Velocity.X, -Velocity.Y * Spring);
        }
    }
}