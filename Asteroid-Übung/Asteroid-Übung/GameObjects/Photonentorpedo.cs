﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Asteroid_Übung.GameObjects
{
    /// <summary>
    ///  <c>Photonentorpedo</c> is a GameObject.
    /// </summary>
    class Photonentorpedo : GameObject
    {
        Polygon umriss = new Polygon();
        double lifetime = 2000;


        /// <summary>
        /// Constructor: uses the <see cref="Ship"/> as a reference Point.
        /// </summary>
        /// <param name="ship">Reference Point for start and direction</param>
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

        public double Lifetime
        { get { return lifetime; } }

        public override void DrawSelf(Canvas zeichenfläche)
        {
            const double TIMEFRAME = 1000 / 30; //30FPS

            double winkelInGrad = Math.Atan2(VY, VX) * 180.0 / Math.PI + 90;
            umriss.RenderTransform = new RotateTransform(winkelInGrad);
            zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, X);
            Canvas.SetTop(umriss, Y);

            this.lifetime -= TIMEFRAME;
        }
    }
}
