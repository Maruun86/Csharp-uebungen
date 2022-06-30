using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dateiwächter
{
    class Message
    {
        public DateTime date;
        string name;

        public Message(string name)
        {
            this.date = DateTime.Now;
            this.name = name;
        }

        public override string ToString()
        {
            return DateTime.Now + " " + name + "\n";
        }
    }
}
