using System;
using System.Collections.Generic;
using System.Text;

namespace Wecker
{
    class Termin
    {
        string title;
        int hours;
        int minutes;

        public Termin (String title, int hours, int minutes)
        {
            this.title = title;
            this.hours = hours;
            this.minutes = minutes;
        }
        //---Getter/Setter
        public string Title
        {
            get { return title; }
            set { title = value; }  
        }
        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }
        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }

        //-------------------
        public override string ToString()
        {
            string s = this.Title + "-" + this.Hours.ToString("D2") + ":" + this.Minutes.ToString("D2");
            return s;
        }
     
    }
}
