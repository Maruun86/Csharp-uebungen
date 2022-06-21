using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Asteroid_Übung
{
    class GameObject  //TODO : abstract
    {
        //vx und vy sind die Geschwindigkeiten in eine Richtung
        double vx;
        double vy;
        double x;
        double y;
        int size;
        bool periodischeFortsetzung = true;

        public GameObject(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public GameObject(double x, double y, double vx, double vy)
        {
            this.x = x;
            this.y = y; 
            this.vx = vx;
            this.vy = vy;
        }
        public virtual void DrawSelf(Canvas zeichenfläche)
        {
            //leer
        }
        public double X
        { get { return x; } set { x = value; } }  
        
        public double Y
        { get { return y; } set { y = value; } }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public void Animate(TimeSpan interval, Canvas zeichenfläche)
        {
            x += vx * interval.TotalSeconds;
            y += vy * interval.TotalSeconds;

            if (periodischeFortsetzung)
            {
                if (x > zeichenfläche.ActualWidth)
                {
                    x = 0 - Size;
                }
                else if (x < 0.0)
                {
                    x = zeichenfläche.ActualWidth;
                }
                if (y > zeichenfläche.ActualHeight)
                {
                    y = 0 - Size;
                }
                else if (y < 0.0)
                {
                    y = zeichenfläche .ActualHeight;
                }
            }
            

        }
    }
    class Asteroid : GameObject
    {
        static Random zufall = new Random();

        public Asteroid (Canvas zeichenfläche)
            : base(zufall.NextDouble() * zeichenfläche.ActualWidth, 
                  zufall.NextDouble() * zeichenfläche.ActualHeight,
                  (zufall.NextDouble() - 0.5) * 100.0, 
                  (zufall.NextDouble() -0.5) * 100.0)
        {
            //leer
        }

        public override void DrawSelf(Canvas zeichenfläche)
        {
            Polygon poly = new Polygon();

            for (int i = 0; i < 20; i++)
            {
                double alpha = 2.0 * Math.PI / 20 * i;
                double radius = 8.0 + 4.0 *zufall.NextDouble();
                poly.Points.Add(new System.Windows.Point(radius * Math.Cos(alpha), radius * Math.Sin(alpha)));
            }

            poly.Fill = Brushes.Gray;
            zeichenfläche.Children.Add(poly);
            Canvas.SetLeft(poly, X);
            Canvas.SetTop(poly, Y);
        }


    }
    /*
    class Ship : GameObject
    {

    }

    class Photonentorpedo : GameObject
    {

    }
    */
}
