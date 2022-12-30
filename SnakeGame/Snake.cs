using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Snake
    {
        public List<Vector2Int> Body;
        public Vector2Int Head
        {
            get
            {
                return Body[Body.Count -1];
            }
        }

        public Vector2Int direction;

        public int Score { get { return Body.Count; } }

        public Snake(Vector2Int position)
        {
            Body = new List<Vector2Int>()
            {
                position
            };

            direction = new Vector2Int(1, 0);
        }

        public bool CheckForOverlap()
        {
            return Body.Distinct().Count() != Body.Count;
        }

        public void Move(bool resize)
        {
            Body.Add(new Vector2Int(Body[Body.Count - 1].X + direction.X, Body[Body.Count - 1].Y + direction.Y));

            if(!resize)
                Body.RemoveAt(0);
        }
    }
}
