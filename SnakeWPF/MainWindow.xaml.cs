using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SnakeGame;

namespace SnakeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int cellSize = 40;
        private const int GRIDSIZE = 10;
        private const int UPDATES_PERSEC = 4;
        private const int DELAY = 1000 / UPDATES_PERSEC;

        KeyEventArgs lastKey;
        private Game game; 

        public MainWindow()
        {
            InitializeComponent();
            Task.Run(()=> PlayGame());
        }

        private void PlayGame()
        {
            game = new Game(new Vector2Int(GRIDSIZE, GRIDSIZE));
            snakeRenderer.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => DrawGame(game)));

            while (true)
            {
                Thread.Sleep(DELAY);
                game.Tick(GetInput());
                snakeRenderer.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => DrawGame(game)));
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            DrawGame(game);
        }

        private Direction GetInput()
        {
            if (lastKey == null)
                return Direction.None;

            switch (lastKey.Key)
            {
                case Key.Left: 
                    return Direction.Left;
                case Key.Right:
                    return Direction.Right;
                case Key.Up:
                    return Direction.Up;
                case Key.Down:
                    return Direction.Down;
                default:
                    return Direction.None;
            }
        }
        
        private void DrawGame(Game game)
        {
            int padding = 5;

            if(grid.ActualWidth < grid.ActualHeight)
            {
                cellSize = ((int)grid.ActualWidth - game.gridSize.X * padding) / (game.gridSize.X); //BS MATH
            }
            else
            {
                cellSize = ((int)grid.ActualHeight - game.gridSize.Y * padding) / (game.gridSize.Y); //BS MATH
            }

            int OffsetX = ((int)grid.ActualWidth - ((cellSize + padding)* game.gridSize.X))/ 2 + padding/2;


            snakeRenderer.Children.Clear();

            snakeRenderer.Background = Brushes.Black;

            for (int y = 0; y < game.gridSize.Y; y++)
                for (int x = 0; x < game.gridSize.X; x++)
                {
                    var position = new Vector2Int(x, y);

                    Rectangle rectangle = new Rectangle
                    {
                        Height = cellSize,
                        Width = cellSize,
                    };

                    SolidColorBrush brush = new SolidColorBrush();

                    if (game.snake.Head.Equals(position))
                    {
                        brush.Color = Color.FromRgb(255, 0, 0);
                    }
                    else if (game.snake.Body.Contains(position))
                    {
                        brush.Color = Color.FromRgb(0, 100, 0);
                    }
                    else if (game.goodiePosition.Equals(position))
                    {
                        brush.Color = Color.FromRgb(255, 255, 0);
                    }
                    else
                    {
                        brush.Color = Color.FromRgb(220, 220, 220);
                    }

                    rectangle.Fill = brush;
                    snakeRenderer.Children.Add(rectangle);

                    Canvas.SetLeft(rectangle, x * (cellSize + padding) + OffsetX);
                    Canvas.SetTop(rectangle, (int)grid.ActualHeight - (y * (cellSize + padding) + cellSize + padding));
                }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape)
                Close();

            lastKey = e;
        }
    }
}
