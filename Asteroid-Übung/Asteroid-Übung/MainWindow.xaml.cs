using Asteroid_Übung.GameObjects;
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
        List<Asteroid> asteroids = new List<Asteroid>();
        List<Photonentorpedo> photonentorpedoes = new List<Photonentorpedo>();
        Ship player;

        DispatcherTimer timer = new DispatcherTimer();
        Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
        
        int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSound();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += GameLoop;
        }

        void GameLoop(object sender, EventArgs e)
        {
            List<GameObject> newGameObjects = gameObjects;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Animate(timer.Interval, zeichenfläche);
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
            //---
            //finde Einträge mit Collision
            foreach (Asteroid a in asteroids)
            {
                foreach (Photonentorpedo p in photonentorpedoes)
                {
                    //Check torpedo Lifetime, wenn abgelaufen entferne
                    if (p.Lifetime <= 0)
                    {
                        checkGameObjects.Remove(p);
                    }
                    if (a.containsPoint(p.X, p.Y))
                    {
                        checkGameObjects.Remove(a);
                        checkGameObjects.Remove(p);

                        //a wird in 2 kleinere aufgeteilt wenn dies nicht schonmal passiert ist
                        if (a.Size == a.SSize)
                        {
                            checkGameObjects.Add(DestroyAsteroid(a));
                            checkGameObjects.Add(DestroyAsteroid(a));
                            Score(100);
                        }else
                        {
                            Score(50);
                        }
                        sounds["explosion-02.wav"].PlaySound();

                    }
                }
                if(a.containsPoint(player.X, player.Y))
                {
                    GameOver();
                }
            }
            return checkGameObjects;
        }
        private void Score(int increase)
        {
            score += increase;
            Label_Score.Content = score;
        }
        private void GameOver()
        {
            //Reset Spielumgebung
            timer.Stop();
            gameObjects.Clear();
            zeichenfläche.Children.Clear();
            start_button.Visibility = Visibility.Visible;
            score = 0;
            Label_Score.Content = score;

            sounds["game-over.wav"].PlaySound();

            MessageBox.Show("Game Over!");      
        }
        private Asteroid DestroyAsteroid(Asteroid a)
        {
            Asteroid ast = new Asteroid(zeichenfläche, a.Size / 2);
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
                asteroids.Add(ast);
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
                        if (player.WeapCD == 0)
                        {
                            const int WEAPON_CD = 1000;
                            Photonentorpedo p = player.Schuss();
                            gameObjects.Add(p);
                            photonentorpedoes.Add(p);

                            player.WeapCD = WEAPON_CD;
                            sounds["laser-shoot.wav"].PlaySound();


                        }
                        break;
                }
            }
        }
        private void InitializeSound()
        {
            Sound s = new Sound("explosion-02.wav");
            sounds.Add(s.Name, s);
            s = new Sound("game-over.wav");
            sounds.Add(s.Name, s);
            s = new Sound("laser-shoot.wav");
            sounds.Add(s.Name, s);
        }

    }
}
