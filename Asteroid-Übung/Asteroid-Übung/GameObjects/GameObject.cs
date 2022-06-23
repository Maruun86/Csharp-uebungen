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
}
