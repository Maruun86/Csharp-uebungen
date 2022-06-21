using System;
using System.Collections.Generic;
using System.Text;

namespace Wecker
{
    abstract class Termin
    {
        string title;
        int hours;
        int minutes;

        public Termin(string title, int hours, int minutes)
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

        public abstract void Ring();

        public bool IsActive()
        {
            DateTime d = DateTime.Now;
            return d.Hour == hours && d.Minute == minutes;
        }
    }


    class TerminColor : Termin
    {
        MainWindow mw;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        //Erbe von Termin und nutze den Konstrukter der Mutterklasse :base(...)
        public TerminColor(string title, int hours, int minutes ,MainWindow mw)
            : base(title, hours, minutes)
        {
            this.mw = mw;
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += RingOff;
        }

        public override string ToString()
        {
            return "F " + base.ToString();
        }

 
        //Timer wird in Ring gestartet und in RingOff gestoppt
        public override void Ring()
        {
            mw.Background = System.Windows.Media.Brushes.Red;
            timer.Start();
        }
        public void RingOff(object sender, EventArgs e)
        {
            mw.Background = System.Windows.Media.Brushes.White;
            timer.Stop();
        }

    }


    class TerminSound : Termin
    {
        public TerminSound(string title, int hours, int minutes)
           : base(title, hours, minutes)
        {

        }
        public override string ToString()
        {
            return "K " + base.ToString();
        }
        public override void Ring()
        {
            System.Media.SystemSounds.Beep.Play();
        }
    }
}
