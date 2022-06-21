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
using System.Windows.Threading;

namespace Asteroid_Übung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Asteroid> asteroids = new List<Asteroid>();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += GameLoop;
            timer.Start();

        }

        void GameLoop(object sender, EventArgs e)
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Animate(timer.Interval,zeichenfläche); 
            }  
            zeichenfläche.Children.Clear();

            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].DrawSelf(zeichenfläche);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Asteroid ast = new Asteroid(zeichenfläche);
                asteroids.Add(ast);
            }



        }
    }
}
