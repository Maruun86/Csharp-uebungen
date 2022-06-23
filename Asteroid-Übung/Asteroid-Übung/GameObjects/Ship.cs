using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Asteroid_Übung.GameObjects
{
    /// <summary>
    ///  <c>Ship</c> is a GameObject and in this game the "player."
    /// </summary>
    class Ship : GameObject
    {
        Polygon umriss = new Polygon();
        int weaponCD = 0;

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
            if (this.weaponCD > 0)
            {
                this.weaponCD -= 100;
            }
        }

        /// <summary>
        /// Used for the direction change of the ship with a <see cref="bool"/>.
        /// </summary>
        /// <param name="nachLinks"> Am i turning towards the left?</param>
        public void BiegeAb(bool nachLinks)
        {
            double winkel = (nachLinks ? -10.0 : 10.0) * Math.PI / 180.0;

            double vxNew = Math.Cos(winkel) * VX - Math.Sin(winkel) * VY;
            VY = Math.Sin(winkel) * VX + Math.Cos(winkel) * VY;
            VX = vxNew;
        }

        /// <summary>
        /// Used for the acceleration and deacceleration of the ship with a <see cref="bool"/>.
        /// </summary>
        /// <param name="beschleunige">Do i accelerate?</param>
        public void Beschleunige(bool beschleunige)
        {
            double faktor = beschleunige ? 1.1 : 0.9;

            VX *= faktor;
            VY *= faktor;
        }
        /// <summary>
        /// Shoots a projectile as a <see cref="Photonentorpedo"/>.
        /// </summary>
        /// <returns><see cref="Photonentorpedo"/></returns>
        public Photonentorpedo Schuss()
        {
            Photonentorpedo newTorpedo = new Photonentorpedo(this);
            return newTorpedo;
        }

        public int WeapCD
        { get { return this.weaponCD; } set { this.weaponCD = value; } }

    }
}
