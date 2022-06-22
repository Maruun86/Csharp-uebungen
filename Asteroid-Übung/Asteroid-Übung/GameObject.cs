using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Asteroid_Übung
{
    abstract class GameObject
    {
        //vx und vy sind die Geschwindigkeiten in eine Richtung
        double vx;
        double vy;
        double x;
        double y;
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

        public abstract void DrawSelf(Canvas zeichenfläche);

        public double X
        { get { return x; } set { x = value; } }
        public double Y
        { get { return y; } set { y = value; } }
        public double VX
        { get { return vx; } set { vx = value; } }
        public double VY
        { get { return vy; } set { vy = value; } }

        public void Animate(TimeSpan interval, Canvas zeichenfläche)
        {
            x += vx * interval.TotalSeconds;
            y += vy * interval.TotalSeconds;

            //Verlassen des Spielfelds bringt das Objekt wieder auf die gegenüberliegende Seite.
            if (periodischeFortsetzung)
            {
                if (x > zeichenfläche.ActualWidth)
                {
                    x = 0;
                }
                else if (x < 0.0)
                {
                    x = zeichenfläche.ActualWidth;
                }
                if (y > zeichenfläche.ActualHeight)
                {
                    y = 0;
                }
                else if (y < 0.0)
                {
                    y = zeichenfläche.ActualHeight;
                }
            }

        }
    }
    class Asteroid : GameObject
    {
        static Random zufall = new Random();
        Polygon umriss = new Polygon();
        double size;
        public Asteroid(Canvas zeichenfläche, double size = 16.0)
            : base(zufall.NextDouble() * zeichenfläche.ActualWidth,
                  zufall.NextDouble() * zeichenfläche.ActualHeight,
                  (zufall.NextDouble() - 0.5) * 100.0,
                  (zufall.NextDouble() - 0.5) * 100.0)
        {
            this.size = size;

            //Einmal wird die Form erstellt
            for (int i = 0; i < 20; i++)
            {
                double alpha = 2.0 * Math.PI / 20 * i;
                double radius = size + 4.0 * zufall.NextDouble();
                umriss.Points.Add(new System.Windows.Point(radius * Math.Cos(alpha), radius * Math.Sin(alpha)));
            }
            umriss.Fill = Brushes.Gray;
        }

        public override void DrawSelf(Canvas zeichenfläche)
        {

            zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, X);
            Canvas.SetTop(umriss, Y);
        }
        public bool containsPoint(double x, double y)
        {
            return umriss.RenderedGeometry.FillContains(new System.Windows.Point(x - X, y - Y));
        }

        public double Size
        {
            get { return size; }
        }
    }

    class Ship : GameObject
    {
        Polygon umriss = new Polygon();

        public Ship(Canvas zeichenfläche)
            : base(0.5 * zeichenfläche.ActualWidth, 0.5 * zeichenfläche.ActualHeight, 5.0, 1.0)
        {
            umriss.Points.Add(new System.Windows.Point(0.0, -15.0));
            umriss.Points.Add(new System.Windows.Point(10.0, 12.0));
            umriss.Points.Add(new System.Windows.Point(-10.0, 12.0));
            umriss.Fill = Brushes.Blue;
        }

        public override void DrawSelf(Canvas zeichenfläche)
        {
            double winkelInGrad = Math.Atan2(VY, VX) * 180.0 / Math.PI + 90;

            umriss.RenderTransform = new RotateTransform(winkelInGrad);
            zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, X);
            Canvas.SetTop(umriss, Y);
        }

        public void BiegeAb(bool nachLinks)
        {
            double winkel = (nachLinks ? -5.0 : 5.0) * Math.PI / 180.0;

            double vxNew = Math.Cos(winkel) * VX - Math.Sin(winkel) * VY;
            VY = Math.Sin(winkel) * VX + Math.Cos(winkel) * VY;
            VX = vxNew;
        }

        public void Beschleunige(bool beschleunige)
        {
            double faktor = beschleunige ? 1.1 : 0.9;

            VX *= faktor;
            VY *= faktor;

        }
        public Photonentorpedo Schuss()
        {
            Photonentorpedo newTorpedo = new Photonentorpedo(this);
            return newTorpedo;

        }
    }
    class Photonentorpedo : GameObject
    {
        Polygon umriss = new Polygon();
        int lifetime = 2000;

        public Photonentorpedo(Ship ship)
            : base(ship.X, ship.Y, ship.VX, ship.VY)
        {
            const int festeLaenge = 200;
            //Vektor normalisierung
            double laenge = Math.Sqrt((Math.Pow(ship.VX, 2) + Math.Pow(ship.VY, 2)));
            VX = ship.VX / laenge * festeLaenge + ship.VX;
            VY = ship.VY / laenge * festeLaenge + ship.VY;

            umriss.Points.Add(new System.Windows.Point(-5.0, -5.0));
            umriss.Points.Add(new System.Windows.Point(5.0, -5.0));
            umriss.Points.Add(new System.Windows.Point(-5.0, 5.0));
            umriss.Points.Add(new System.Windows.Point(5.0, 5.0));
            umriss.Fill = Brushes.Red;
        }

        public int Lifetime
        { get { return lifetime; } }

        public override void DrawSelf(Canvas zeichenfläche)
        {
            double winkelInGrad = Math.Atan2(VY, VX) * 180.0 / Math.PI + 90;
            umriss.RenderTransform = new RotateTransform(winkelInGrad);
            zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, X);
            Canvas.SetTop(umriss, Y);
            this.lifetime -= 100;
        }
    }

}
