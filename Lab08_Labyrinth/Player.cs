using System.Windows.Media;
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