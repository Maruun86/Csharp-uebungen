using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace Asteroid_Übung
{
    /// <summary>
    /// A class for soundfiles.
    /// </summary>
    class Sound
    {
        string name;
        FileStream stream;
        SoundPlayer newSound; 

        /// <summary>
        /// Creates a new Sound, turning it into a Filestream and creates a Soundplayer
        /// </summary>
        /// <param name="name">The name of the Soundfile as a <see cref="string"/>. Example:"example.wav"</param>
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
