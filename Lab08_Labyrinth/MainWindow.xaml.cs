using System;                    // Grundfunktionen (z.B. Math)
using System.Windows;            // WPF Basis
using System.Windows.Controls;
using System.Windows.Input;      // Tastatureingaben
using System.Windows.Media;      // Farben
using System.Windows.Shapes;     // Rechtecke, Ellipsen

namespace Labyrinth
{
    public partial class MainWindow : Window
    {
        // 2D Array → speichert das Labyrinth
        // 1 = Wand
        // 0 = Weg
        // 2 = Ziel
        int[,] labyrinth =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1},
            {1,0,1,0,1,0,1,1,1,0,1,0,1,0,1},
            {1,0,1,0,0,0,1,0,1,0,0,0,1,0,1},
            {1,0,1,1,1,0,1,0,1,1,1,0,1,0,1},
            {1,0,0,0,1,0,0,0,0,0,1,0,1,0,1},
            {1,1,1,0,1,1,1,1,1,0,1,0,1,0,1},
            {1,0,0,0,0,0,0,0,1,0,1,0,0,0,1},
            {1,0,1,1,1,1,1,0,1,0,1,1,1,0,1},
            {1,0,0,0,0,0,1,0,0,0,0,0,1,0,1},
            {1,1,1,1,1,0,1,1,1,1,1,0,1,0,1},
            {1,0,0,0,1,0,0,0,0,0,1,0,0,0,1},
            {1,0,1,0,1,1,1,1,1,0,1,1,1,0,1},
            {1,0,1,0,0,0,0,0,1,0,0,0,0,2,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        int cellSize = 40;   // Größe eines Feldes in Pixel
        int viewRadius = 2;  // Sichtweite des Spielers

        Player player;       // Referenz auf Player-Objekt

        public MainWindow()
        {
            InitializeComponent();  // Baut das XAML-Fenster

            CreatePlayer();         // Spieler erstellen
            DrawGame();             // Spielfeld zeichnen
        }

        private void CreatePlayer()
        {
            // Spieler startet bei Feld (1,1)
            player = new Player(1, 1, cellSize);
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();
            // Canvas wird komplett geleert
            // Wichtig: sonst würden alte Zeichnungen bleiben

            for (int row = 0; row < labyrinth.GetLength(0); row++)
            {
                for (int col = 0; col < labyrinth.GetLength(1); col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = cellSize;
                    rect.Height = cellSize;

                    // Manhattan-Distanz berechnen
                    int distance = Math.Abs(row - player.Y) +
                                   Math.Abs(col - player.X);

                    // Wenn Feld im Sichtbereich liegt
                    if (distance <= viewRadius)
                    {
                        if (labyrinth[row, col] == 1)
                            rect.Fill = Brushes.DarkSlateGray; // Wand
                        else if (labyrinth[row, col] == 2)
                            rect.Fill = Brushes.LimeGreen;     // Ziel
                        else
                            rect.Fill = Brushes.WhiteSmoke;    // Weg
                    }
                    else
                    {
                        rect.Fill = Brushes.Black; // außerhalb Sicht
                    }

                    // Position des Rechtecks im Canvas
                    Canvas.SetLeft(rect, col * cellSize);
                    Canvas.SetTop(rect, row * cellSize);

                    GameCanvas.Children.Add(rect);
                }
            }

            // Spieler wird zuletzt gezeichnet
            // damit er über dem Labyrinth liegt
            Canvas.SetLeft(player.Shape, player.X * cellSize);
            Canvas.SetTop(player.Shape, player.Y * cellSize);
            GameCanvas.Children.Add(player.Shape);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = player.X;
            int newY = player.Y;

            // Bewegung je nach Taste
            if (e.Key == Key.W) newY--;
            if (e.Key == Key.S) newY++;
            if (e.Key == Key.A) newX--;
            if (e.Key == Key.D) newX++;

            // Prüfen ob neue Position im Array liegt
            if (newY >= 0 && newY < labyrinth.GetLength(0) &&
                newX >= 0 && newX < labyrinth.GetLength(1))
            {
                // Nur bewegen wenn kein Wandfeld
                if (labyrinth[newY, newX] != 1)
                {
                    player.X = newX;
                    player.Y = newY;

                    // Wenn Ziel erreicht
                    if (labyrinth[newY, newX] == 2)
                    {
                        MessageBox.Show("Du hast das Labyrinth geschafft! 🎉");
                    }

                    DrawGame(); // Neu zeichnen wegen Sicht
                }
            }
        }
    }
}