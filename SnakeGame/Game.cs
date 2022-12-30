using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Game
    {
        private readonly Vector2Int[] rotations = { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };

        private readonly Random rnd;

        public bool IsDead;

        public Snake snake;
        public Vector2Int gridSize;

        public Vector2Int goodiePosition;
        public Game(Vector2Int gameSize)
        {
            gridSize = gameSize;
            rnd = new Random();
            Reset();
        }
        public void Reset()
        {
            snake = new Snake(new Vector2Int(gridSize.X/2, gridSize.X/2));
            IsDead = false;
            rotationIndex = 1;
            PlaceGoodie();
        }

        public void PlaceGoodie()
        {
            if (snake.Body.Count >= gridSize.X * gridSize.Y)
                return;

            while (true)
            {
                Vector2Int tryPos = new Vector2Int(rnd.Next(0, gridSize.X), rnd.Next(0, gridSize.Y));
                if (!snake.Body.Contains(tryPos))
                {
                    goodiePosition = tryPos;
                    return;
                }
            }
        }

        int rotationIndex;

        public void Tick(Direction turn)
        {
            if (IsDead)
                Reset();

            
            if(turn != Direction.None && (int)(turn + 2) % 4 != rotationIndex)
            {
                rotationIndex = (int)turn;
                snake.direction = rotations[rotationIndex];
            }

            if (snake.Head.Equals(goodiePosition))
            {
                snake.Move(true);
                PlaceGoodie();
            }
            else
            {
                snake.Move(false);
            }
            
            if (snake.Head.X < 0 || snake.Head.X >= gridSize.X || snake.Head.Y < 0 || snake.Head.Y >= gridSize.Y || snake.CheckForOverlap())
            {
                IsDead = true;
            }
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        None,
    }
}
