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

namespace PONG
{
    public partial class MainWindow : Window
    {
        
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        double vx = 90.0;
        double vy = 110.0;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.IsEnabled = true;
            timer.Tick += animate;
        }
       void animate(object sender, EventArgs e)
        {
            double x = Canvas.GetLeft(ball);
            x += vx * timer.Interval.TotalSeconds;
            Canvas.SetLeft(ball, x);

            double y = Canvas.GetTop(ball);
            if (y <= 0.0 | y >= myCanvas.ActualHeight - ball.Height)
            {
                vy = vy * -1;
            }

            y += vy * timer.Interval.TotalSeconds;
            
            Canvas.SetTop(ball, y);
        }
    }
}