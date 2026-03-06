/*using System.Windows.Media;
using System.Windows.Shapes;

namespace Labyrinth
{
    public class Player
    {
        public int X { get; set; }   // X Position im Grid
        public int Y { get; set; }   // Y Position im Grid

        public Ellipse Shape { get; set; }  // Grafische Darstellung

        public Player(int startX, int startY, int size)
        {
            X = startX;
            Y = startY;

            Shape = new Ellipse();
            Shape.Width = size * 0.8;   // etwas kleiner als Feld
            Shape.Height = size * 0.8;
            Shape.Fill = Brushes.CornflowerBlue;
        }
    }
}
*/

using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Labyrinth
{
    public class Player
    {
        public int X;
        public int Y;

        public Image Shape;

        public Player(int x, int y, int size)
        {
            X = x;
            Y = y;

            Shape = new Image();
            Shape.Width = size;
            Shape.Height = size;

            // HIER SPIELERBILD
            Shape.Source = new BitmapImage(new Uri("player.png", UriKind.Relative));
        }
    }
}