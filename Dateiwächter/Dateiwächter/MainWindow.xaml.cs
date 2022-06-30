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

namespace Dateiwächter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

/* Unter Videoanleitung von Prof. Jörn Loviscach
 * https://j3l7h2.de/videos/v.php?v=edNHizpGUIc
 */

public partial class MainWindow : Window
{
        Guardian guardian = new Guardian();
        AboInDatei d1 = new AboInDatei("log.txt");
        AboInDatei d2 = new AboInDatei("log2.txt");
        AboInDatei d3 = new AboInDatei("log3.txt");
        AboInApp d4;

        public MainWindow()
    {
        guardian.SetAbo(d1);
        guardian.SetAbo(d2);
        guardian.SetAbo(d3);
        d4 = new AboInApp(this);
        guardian.SetAbo(d4);
        InitializeComponent();
            
    }
}
}
