using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Labyrinth
{
    public partial class MainWindow : Window
    {
        int[,] labyrinth =
  {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1},
            {1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,1},
            {1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,3,1,0,1,0,1},
            {1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1},
            {1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1},
            {1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1},
            {1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1},
            {1,0,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1},
            {1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1},
            {1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1},
            {1,1,0,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1},
            {1,1,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1},
            {1,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1},
            {1,0,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1},
            {1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1},
            {1,1,1,1,1,1,1,0,1,1,1,0,1,0,1,0,1,1,1,0,1},
            {1,0,2,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1},
            {1,0,1,1,1,0,1,1,1,0,1,1,1,1,1,1,1,0,1,0,1},
            {1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

        int cellSize = 40;
        int viewRadius = 39999;

        Player player;

        public MainWindow()
        {
            InitializeComponent();
            
            CreatePlayer();
            DrawGame();
        }

        private void CreatePlayer()
        {
            player = new Player(1, 1, cellSize);
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            for (int row = 0; row < labyrinth.GetLength(0); row++)
            {
                for (int col = 0; col < labyrinth.GetLength(1); col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = cellSize;
                    rect.Height = cellSize;

                    int distance = Math.Abs(row - player.Y) +
                                   Math.Abs(col - player.X);

                    if (distance <= viewRadius)
                    {
                        if (labyrinth[row, col] == 1)
                            rect.Fill = Brushes.DarkSlateGray;
                        else if (labyrinth[row, col] == 2)
                            rect.Fill = Brushes.LimeGreen;
                        else if (labyrinth[row, col] == 3)
                            rect.Fill = Brushes.Gold;
                        else
                            rect.Fill = Brushes.WhiteSmoke;
                    }
                    else
                    {
                        rect.Fill = Brushes.Black;
                    }

                    Canvas.SetLeft(rect, col * cellSize);
                    Canvas.SetTop(rect, row * cellSize);

                    GameCanvas.Children.Add(rect);
                }
            }

            Canvas.SetLeft(player.Shape, player.X * cellSize);
            Canvas.SetTop(player.Shape, player.Y * cellSize);
            GameCanvas.Children.Add(player.Shape);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = player.X;
            int newY = player.Y;

            if (e.Key == Key.W) newY--;
            if (e.Key == Key.S) newY++;
            if (e.Key == Key.A) newX--;
            if (e.Key == Key.D) newX++;

            if (newY >= 0 && newY < labyrinth.GetLength(0) &&
                newX >= 0 && newX < labyrinth.GetLength(1))
            {
                if (labyrinth[newY, newX] != 1)
                {
                    player.X = newX;
                    player.Y = newY;

                    if (labyrinth[newY, newX] == 2)
                    {
                        MessageBox.Show("Du hast das Labyrinth geschafft! 🎉");
                    }

                    if (labyrinth[newY, newX] == 3)
                    {
                        viewRadius += 3;
                        MessageBox.Show("👀 Sichtbonus gefunden!");

                        labyrinth[newY, newX] = 0;
                    }

                    DrawGame();
                }
            }
        }
    }
}


