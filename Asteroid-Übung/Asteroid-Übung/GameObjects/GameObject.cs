using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Asteroid_Übung
{
    /// <summary>
    ///  <c>GameObject</c> is a abstract Class for every GameObject thats being used in the game />
    /// </summary>
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
        /// <summary>
        /// <c>Abstract: is individuell defined for the child-classes and handles the visual representation of the object</c>
        /// </summary>
        /// <param name="zeichenfläche"><see cref="Canvas"/>the Canvas thats being used</param>
        public abstract void DrawSelf(Canvas zeichenfläche);

        public double X
        { get { return x; } set { x = value; } }
        public double Y
        { get { return y; } set { y = value; } }
        public double VX
        { get { return vx; } set { vx = value; } }
        public double VY
        { get { return vy; } set { vy = value; } }

        /// <summary>
        /// Is for the movement of the Object.
        /// </summary>
        /// <param name="interval"> <see cref="TimeSpan"/> The timespan for the movement needed</param>
        /// <param name="zeichenfläche"> <see cref="Canvas"/>the Canvas thats being used</param>
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
}
