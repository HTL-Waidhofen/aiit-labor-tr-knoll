using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab05_Rechtecke_zeichnen
{
    // Diese Klasse beschreibt ein einzelnes Rechteck
    class Rectangle2D
    {
        // Position der linken oberen Ecke
        public int X { get; private set; }
        public int Y { get; private set; }

        // Breite und Höhe des Rechtecks
        public int Width { get; }
        public int Height { get; }

        // Konstruktor: erstellt ein neues Rechteck
        public Rectangle2D(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Prüft, ob sich dieses Rechteck mit einem anderen überschneidet
        public bool Intersects(Rectangle2D other)
        {
            // Wenn eines komplett links, rechts, oben oder unten liegt → keine Überschneidung
            return !(X + Width <= other.X ||
                     X >= other.X + other.Width ||
                     Y + Height <= other.Y ||
                     Y >= other.Y + other.Height);
        }

        // Prüft, ob das Rechteck bewegt werden darf
        public bool CanMove(int dx, int dy, List<Rectangle2D> rectangles, int maxWidth, int maxHeight)
        {
            // Neue Testposition berechnen
            int newX = X + dx;
            int newY = Y + dy;

            // Prüfen, ob das Rechteck den Bildschirm verlässt
            if (newX < 0 || newY < 0 ||
                newX + Width > maxWidth ||
                newY + Height > maxHeight)
                return false;

            // Testrechteck erstellen
            Rectangle2D test = new Rectangle2D(newX, newY, Width, Height);

            // Prüfen, ob es ein anderes Rechteck schneiden würde
            return !rectangles.Any(r => r != this && test.Intersects(r));
        }

        // Bewegt das Rechteck
        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        // Zeichnet das Rechteck in den Zeichenpuffer
        public void Draw(char[,] canvas)
        {
            // Obere und untere Linie
            for (int i = 0; i < Width; i++)
            {
                canvas[Y, X + i] = '*';
                canvas[Y + Height - 1, X + i] = '*';
            }

            // Linke und rechte Linie
            for (int i = 0; i < Height; i++)
            {
                canvas[Y + i, X] = '*';
                canvas[Y + i, X + Width - 1] = '*';
            }
        }
    }

    // Diese Klasse stellt die Zeichenfläche dar
    class Canvas
    {
        public int Width { get; }
        public int Height { get; }
        private char[,] buffer;

        // Konstruktor: erzeugt eine leere Zeichenfläche
        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            buffer = new char[height, width];
        }

        // Löscht den gesamten Bildschirm
        public void Clear()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    buffer[y, x] = ' ';
        }

        // Gibt den Inhalt des Puffers auf der Konsole aus
        public void Render()
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write(buffer[y, x]);
                Console.WriteLine();
            }
        }

        // Zugriff auf den Zeichenpuffer
        public char[,] Buffer => buffer;
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("(c) 2026 KNOLL Tristan ALL RIGHTS RESERVED");
            Canvas canvas = new Canvas(80, 25);
            List<Rectangle2D> rectangles = new List<Rectangle2D>();

            // Abfrage der Anzahl der Rechtecke
            Console.Write("Wie viele Rechtecke sollen gezeichnet werden? ");
            int count = int.Parse(Console.ReadLine());

            // Eingabe der Rechtecke
            for (int i = 0; i < count; i++)
            {
                Rectangle2D rect;
                bool valid;

                do
                {
                    valid = true;

                    Console.WriteLine($"\nRechteck {i + 1}");

                    Console.Write("X (linke obere Ecke): ");
                    int x = int.Parse(Console.ReadLine());

                    Console.Write("Y (linke obere Ecke): ");
                    int y = int.Parse(Console.ReadLine());

                    Console.Write("Breite: ");
                    int width = int.Parse(Console.ReadLine());

                    Console.Write("Höhe: ");
                    int height = int.Parse(Console.ReadLine());

                    rect = new Rectangle2D(x, y, width, height);

                    // Prüfen auf Bildschirmrand
                    if (x < 0 || y < 0 ||
                        x + width > canvas.Width ||
                        y + height > canvas.Height)
                    {
                        Console.WriteLine("❌ Rechteck ist außerhalb des Bildschirms!");
                        valid = false;
                    }

                    // Prüfen auf Überschneidung mit bestehenden Rechtecken
                    if (rectangles.Any(r => rect.Intersects(r)))
                    {
                        Console.WriteLine("❌ Rechteck überschneidet ein anderes!");
                        valid = false;
                    }

                } while (!valid);

                rectangles.Add(rect);
            }

            // Das zuletzt gezeichnete Rechteck ist aktiv
            Rectangle2D active = rectangles.Last();

            // Hauptschleife für Bewegung
            while (true)
            {
                canvas.Clear();

                // Alle Rechtecke zeichnen
                foreach (var r in rectangles)
                    r.Draw(canvas.Buffer);

                canvas.Render();

                ConsoleKey key = Console.ReadKey(true).Key;

                // Bewegung über WASD oder Pfeiltasten
                if ((key == ConsoleKey.A || key == ConsoleKey.LeftArrow) &&
                    active.CanMove(-1, 0, rectangles, canvas.Width, canvas.Height))
                    active.Move(-1, 0);

                if ((key == ConsoleKey.D || key == ConsoleKey.RightArrow) &&
                    active.CanMove(1, 0, rectangles, canvas.Width, canvas.Height))
                    active.Move(1, 0);

                if ((key == ConsoleKey.W || key == ConsoleKey.UpArrow) &&
                    active.CanMove(0, -1, rectangles, canvas.Width, canvas.Height))
                    active.Move(0, -1);

                if ((key == ConsoleKey.S || key == ConsoleKey.DownArrow) &&
                    active.CanMove(0, 1, rectangles, canvas.Width, canvas.Height))
                    active.Move(0, 1);

                // ESC beendet das Programm
                if (key == ConsoleKey.Escape)
                    break;
            }
        }
    }
}

