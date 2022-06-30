using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Dateiwächter
{
    abstract class Abonnent
    {
        abstract public void Notify(string s);

    }

    class AboInDatei : Abonnent
    {
        string pathToFile;

        public AboInDatei(string path)
        {
            this.pathToFile = path;
        }
        public override void Notify(string s)
        {
            File.AppendAllText(pathToFile, s);
        }
    }
    class AboInApp : Abonnent
    {
        MainWindow w;

        public AboInApp(MainWindow w)
        {
            this.w = w;
        }
        public override void Notify(string s)
        {
            w.Dispatcher.Invoke( 
                () => { w.textBlock.Text += s;
                    w.WindowState = System.Windows.WindowState.Normal;
                    w.Activate();
                }
                    
                );
        }
    }
}
