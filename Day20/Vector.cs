namespace Day20
{
    class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Vector(string raw)
        {
            var parts = raw.Substring(3, raw.Length - 4).Split(',');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);
            Z = int.Parse(parts[2]);
        }
        public static Vector operator +(Vector left, Vector right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }
        public static bool operator ==(Vector left, Vector right)
        {
            return left.X == right.X
                && left.Y == right.Y
                && left.Z == right.Z;
        }
        public static bool operator !=(Vector left, Vector right)
        {
            return left.X != right.X
                || left.Y != right.Y
                || left.Z != right.Z;
        }
        public override bool Equals(object obj)
        {
            return this == (Vector)obj;
        }
        public override int GetHashCode()
        {
            return $"{X},{Y},{Z}".GetHashCode();
        }
    }
}