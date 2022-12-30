using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public struct Vector2Int : IEquatable<Vector2Int>
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Vector2Int other)
        {
            if (other.X == this.X && other.Y == this.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"({ X }, { Y })";
        }
    }
}
