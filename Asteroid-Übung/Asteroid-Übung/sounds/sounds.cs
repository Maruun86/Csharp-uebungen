using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace Asteroid_Übung
{
    class Sound
    {
        string name;
        FileStream stream;
        SoundPlayer newSound; 

        public Sound(string name) 
        {
            this.name = name;
            stream = File.Open("../../../sounds/" + name, FileMode.Open);
            newSound = new SoundPlayer(stream);
        }

         public void PlaySound()
        {          
            newSound.Load();
            newSound.Play();

        }
        public string Name
        { get { return name; } }
    }
}
