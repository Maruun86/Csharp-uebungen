using Asteroid_Übung.GameObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Asteroid_Übung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
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

        /// <summary>
        /// MainWindow
        /// </summary>
        public MainWindow()
        {
            const double TIMESPAN = 1000 / 30 ; // 30FPS
            InitializeComponent();
            InitializeSound();
            timer.Interval = TimeSpan.FromMilliseconds(TIMESPAN);
            timer.Tick += GameLoop;
        }

        /// <summary>
        /// Is used for the standard Gameloop being called by the timer.Tick.
        /// </summary>
        /// <param name="sender"> <see cref="object"/> from timer.tick</param>
        /// <param name="e"> <see cref="EventArgs"/> from timer.Tick</param>
        void GameLoop(object sender, EventArgs e)
        {
            List<GameObject> newGameObjects = new List<GameObject>(gameObjects);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                //Check torpedo Lifetime, wenn abgelaufen entferne
                if (gameObjects[i].GetType() == typeof(Photonentorpedo))
                {
                    Photonentorpedo p = (Photonentorpedo)gameObjects[i];
                    if (p.Lifetime <= 0)
                    {
                        newGameObjects.Remove(p);
                        photonentorpedoes.Remove(p);
                    }
                } 
                gameObjects[i].Animate(timer.Interval, zeichenfläche);

            }
            gameObjects = newGameObjects;
            newGameObjects = this.CheckCollision();
            zeichenfläche.Children.Clear();
            if(gameObjects.Count == 1)
            {
                GameWon();
            }
            //alte Liste mit Änderungen der neuen Liste überschrieben

            gameObjects.ForEach(x => x.DrawSelf(zeichenfläche));
        }
        /// <summary>
        /// Collision check for EVERYTHING.
        /// </summary>
        /// <returns> A List of <see cref="gameObjects"/> will be returned</returns>
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
            //finde Einträge mit Collision.
            foreach (Asteroid a in asteroids)
            {
                foreach (Photonentorpedo p in photonentorpedoes)
                {                   
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

        /// <summary>
        /// You score a point this one fires off.
        /// </summary>
        /// <param name="increase"> The amount of points the score rises</param>
        private void Score(int increase)
        {
            score += increase;
            Label_Score.Content = score;
        }

        /// <summary>
        /// Function for a game reset.
        /// </summary>
        private void GameReset()
        {
            //Reset Spielumgebung
            timer.Stop();
            gameObjects.Clear();
            photonentorpedoes.Clear();
            asteroids.Clear();
            zeichenfläche.Children.Clear();
          
        }
        /// <summary>
        /// Victory function
        /// </summary>
        private void GameWon()
        {
            GameReset();
            sounds["victory.wav"].PlaySound();
            MessageBox.Show("Du hast " +score+ " Punkte erreicht!");
            start_button.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Game Over Function, when the game ends it ends here.
        /// </summary>
        private void GameOver()
        {
            GameReset();
            sounds["game-over.wav"].PlaySound();
            MessageBox.Show("Game Over!");
            start_button.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Asteroid gets destroid it will spplit into to half the size.
        /// </summary>
        /// <param name="a"></param>
        /// <returns> back the new <see cref="Asteroid"/></returns>
        private Asteroid DestroyAsteroid(Asteroid a)
        {
            Asteroid ast = new Asteroid(zeichenfläche, a.Size / 2);
            ast.X = a.X;
            ast.Y = a.Y;

            return ast;
        }

        /// <summary>
        /// Button Event simple the start button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            score = 0;
            Label_Score.Content = score;

        }

        /// <summary>
        /// Player controlls also a Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Loading the Sounds for use.
        /// </summary>
        private void InitializeSound()
        {
            Sound s = new Sound("explosion-02.wav");
            sounds.Add(s.Name, s);
            s = new Sound("game-over.wav");
            sounds.Add(s.Name, s);
            s = new Sound("laser-shoot.wav");
            sounds.Add(s.Name, s);
            s = new Sound("victory.wav");
            sounds.Add(s.Name, s);
        }

    }
}
