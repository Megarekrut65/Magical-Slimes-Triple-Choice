using System;

namespace FightingMode.Lobby.Global
{
    public class Point
    {
        public float x;
        public float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Distance(Point other)
        {
            return (float)Math.Sqrt(Math.Pow(x - other.x,2) + Math.Pow(y - other.y, 2));
        }
    }
}