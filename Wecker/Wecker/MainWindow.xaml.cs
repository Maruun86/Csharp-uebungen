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

namespace Wecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();
            timer.Tick += checkAlarms;
        }

        void checkAlarms(object sender, EventArgs e)
        {
            foreach (var item in ListBoxTermine.Items)
            {
                Termin t = (Termin)item;
                if (t.IsActive())
                {
                    t.Ring();
                }
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            string title = textBoxTitle.Text;

            int hours;
            bool result = int.TryParse(textBoxHours.Text, out hours);
            int minutes;
            result = int.TryParse(textBoxMinutes.Text, out minutes);
            
            if (result && hours >= 0 && hours <= 23 && minutes >= 0 & minutes <= 59 )
            {
                CreateTermin(title, hours, minutes);
            }
            else
            {
                MessageBox.Show("Es gab ein Fehler, überprüfen Sie ihre Angaben");
            }
        }

        private void CreateTermin(string title, int hours, int minutes)
        {
            if (radioButtonColour.IsChecked == true)
            {
                Termin newTermin = new TerminColor(title, hours, minutes, this); //MainWindow wird mitgegeben
                ListBoxTermine.Items.Add(newTermin);
            }
            if (radioButtonSound.IsChecked == true)
            {
                Termin newTermin = new TerminSound(title, hours, minutes);
                ListBoxTermine.Items.Add(newTermin);
            }
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            int i = ListBoxTermine.SelectedIndex;
            if (i >= 0)
            {
                ListBoxTermine.Items.RemoveAt(i);
            }
        }
    }
}
