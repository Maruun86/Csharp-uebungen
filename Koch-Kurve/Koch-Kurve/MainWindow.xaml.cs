using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Koch_Kurve
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        double[] x = {0.0, 300.0, 150.0};
        double[] y = {Math.Sqrt((300.0*300.0)-(150.0*150.0)), Math.Sqrt((300.0 * 300.0) - (150.0 * 150.0)), 0}; //Gleichseitiges Dreieck
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double[] xNew = new double[x.Length * 4];
            double[] yNew = new double[x.Length * 4];

            for (int i = 0; i < x.Length; i++)
            {
                xNew[4 * i] = x[i];
                yNew[4 * i] = y[i];

                double dx = x[(i + 1) % x.Length] - x[i];
                double dy = y[(i + 1) % y.Length] - y[i];
                xNew[4 * i + 1] = x[i] +  dx / 3.0; 
                yNew[4 * i + 1] = y[i] +  dy / 3.0;
                xNew[4 * i + 3] = x[i] + 2.0 * dx / 3.0;
                yNew[4 * i + 3] = y[i] + 2.0 * dy / 3.0; 

                double a  = dx / 6.0 - Math.Sqrt(3.0) / 6* dy ;
                double b  = dy / 6.0 + Math.Sqrt(3.0) / 6* dx; ;

                xNew[4 * i + 2] = xNew[4 * i + 1] + a ;
                yNew[4 * i + 2] = yNew[4 * i + 1] + b ;
            }

            x = xNew; 
            y = yNew;

            if (myGrid.Children.Count > 1)
            {
                myGrid.Children.RemoveAt(1);
            }
            
            Polygon polygon = new Polygon();

            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 0.5;

            for (int i = 0; i < x.Length; i++)
            {
                polygon.Points.Add(new Point(x[i], y[i]));
            }
            myGrid.Children.Add(polygon);
        }
    }
}
