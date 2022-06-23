using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Asteroid_Übung.GameObjects
{

    /// <summary>
    ///  <c>Asteroid</c> is a GameObject, saves everything needed for the Asteroid
    /// </summary>
    class Asteroid : GameObject
    {
        static Random zufall = new Random();
        Polygon umriss = new Polygon();
        double size;
        const double STANDARD_SIZE = 16.0;

        /// <summary>
        /// <c>Construktor</c>
        /// </summary>
        /// <param name="zeichenfläche">Canvas for your Asteroid</param> 
        /// <param name="size"> Optonal: otherwise will use <c>STANDARD_SIZE</c></param>
        public Asteroid(Canvas zeichenfläche, double size = STANDARD_SIZE)
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

        /// <summary>
        /// Uses the parameters and its own X,Y Coordinates of another <see cref="GameObject"/> to determin a collision.
        /// </summary>
        /// <param name="x">X-Coordinates of the target <see cref="GameObject"/></param>
        /// <param name="y">Y-Coordinates of the target <see cref="GameObject"/></param>
        /// <returns></returns>
        public bool containsPoint(double x, double y)
        {
            return umriss.RenderedGeometry.FillContains(new System.Windows.Point(x - X, y - Y));
        }

        public double Size
        {
            get { return size; }
        }
        public double SSize
        { get { return STANDARD_SIZE; } }
    }

}
