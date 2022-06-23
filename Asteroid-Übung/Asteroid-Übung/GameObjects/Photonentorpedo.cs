using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Asteroid_Übung.GameObjects
{
    class Photonentorpedo : GameObject
    {
        Polygon umriss = new Polygon();
        int lifetime = 2000;

        public Photonentorpedo(Ship ship)
            : base(ship.X, ship.Y, ship.VX, ship.VY)
        {
            const int FESTE_LAENGE = 200;

            //Vektor normalisierung
            double laenge = Math.Sqrt((Math.Pow(ship.VX, 2) + Math.Pow(ship.VY, 2)));
            VX = ship.VX / laenge * FESTE_LAENGE + ship.VX;
            VY = ship.VY / laenge * FESTE_LAENGE + ship.VY;

            umriss.Points.Add(new System.Windows.Point(-2.0, -2.0));
            umriss.Points.Add(new System.Windows.Point(2.0, -2.0));
            umriss.Points.Add(new System.Windows.Point(2.0, 2.0));
            umriss.Points.Add(new System.Windows.Point(-2.0, 2.0));
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
