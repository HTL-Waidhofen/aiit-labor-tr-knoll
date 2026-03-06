
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Labyrinth
{
    public class Enemy
    {
        public int X;
        public int Y;

        public Image Shape;

        public string Question;
        public int CorrectAnswer;
        public string UniversalAnswer;

        Random rnd = new Random();

        public Enemy(int x, int y, string image, string question, int answer, string universal)
        {
            X = x;
            Y = y;

            Question = question;
            CorrectAnswer = answer;
            UniversalAnswer = universal;

            Shape = new Image();
            Shape.Width = 40;
            Shape.Height = 40;

            Shape.Source = new BitmapImage(new Uri(image, UriKind.Relative));
        }

        public void Move(int[,] labyrinth)
        {
            int dir = rnd.Next(4);

            int newX = X;
            int newY = Y;

            if (dir == 0) newY--;
            if (dir == 1) newY++;
            if (dir == 2) newX--;
            if (dir == 3) newX++;

            if (newY >= 0 && newY < labyrinth.GetLength(0) &&
                newX >= 0 && newX < labyrinth.GetLength(1))
            {
                if (labyrinth[newY, newX] != 1)
                {
                    X = newX;
                    Y = newY;
                }
            }
        }
    }
}

