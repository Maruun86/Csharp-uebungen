using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Asteroid_Übung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<GameObject> gameObjects = new List<GameObject>();
        DispatcherTimer timer = new DispatcherTimer();
        Ship player;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += GameLoop;
        }

        void GameLoop(object sender, EventArgs e)
        {
            List<GameObject> newGameObjects = gameObjects;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Animate(timer.Interval, zeichenfläche);

                //Lifetime check für Torpedos und löschen
                if (gameObjects[i].GetType() == typeof(Photonentorpedo))
                {
                    Photonentorpedo torp = (Photonentorpedo)newGameObjects[i];
                    if (torp.Lifetime <= 0)
                    {
                        newGameObjects.Remove(newGameObjects[i]);
                    }
                }
            }
            gameObjects = newGameObjects;
            newGameObjects = this.CheckCollision();
            zeichenfläche.Children.Clear();
            //alte Liste mit Änderungen der neuen Liste überschrieben

           
            gameObjects.ForEach(x => x.DrawSelf(zeichenfläche));
        }
        private List<GameObject> CheckCollision()
        {
           List<Asteroid> asteroids = new List<Asteroid>();
           List<Photonentorpedo> photonentorpedoes = new List<Photonentorpedo>();
            List<GameObject> checkGameObjects = gameObjects;
            //Vorbereitung für Check Split Einträge
            foreach (GameObject a in gameObjects)
            {
                if (a.GetType() == typeof(Asteroid))
                {
                    asteroids.Add((Asteroid)a);
                }
                else if (a.GetType() == typeof(Photonentorpedo))
                {
                    photonentorpedoes.Add((Photonentorpedo)a);
                }
                
            }
            //finde Einträge mit Collision und
            foreach (Asteroid a in asteroids)
            {
                foreach (Photonentorpedo p in photonentorpedoes)
                {
                    if (a.containsPoint(p.X, p.Y))
                    {
                        checkGameObjects.Remove(a);
                        checkGameObjects.Remove(p);
                        //a wird in 2 kleinere aufgeteilt
                        checkGameObjects.Add(DestroyAsteroid(a));
                        checkGameObjects.Add(DestroyAsteroid(a));

                    }
                    
                }

            }
            return checkGameObjects;
        }
        private Asteroid DestroyAsteroid(Asteroid a)
        {
            Asteroid ast = new Asteroid(zeichenfläche,a.Size / 2);
            ast.X = a.X;
            ast.Y = a.Y;

            return ast;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player = new Ship(zeichenfläche);
            gameObjects.Add(player);

            for (int i = 0; i < 10; i++)
            {
                Asteroid ast = new Asteroid(zeichenfläche);
                gameObjects.Add(ast);
            }

            start_button.Visibility = Visibility.Hidden;
            timer.Start();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Left:
                    case Key.A:
                        player.BiegeAb(true);
                        break;

                    case Key.Right:
                    case Key.D:
                        player.BiegeAb(false);
                        break;

                    case Key.Up:
                    case Key.W:
                        player.Beschleunige(true);
                        break;

                    case Key.Down:
                    case Key.S:
                        player.Beschleunige(false);
                        break;

                    case Key.Space:
                        gameObjects.Add(player.Schuss());//Rückgabe: Photonentorpedo
                        break;
                }
            }
        }
    }
}
