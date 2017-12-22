using System;

namespace Day20
{
    class Particle
    {
        public int ID { get; set; }
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Acceleration { get; set; }
        public int ManhattanDistance => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
        internal void Tick()
        {
            Velocity += Acceleration;
            Position += Velocity;
        }
    }
}