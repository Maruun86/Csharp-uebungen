using System;
using System.Collections.Generic;
using System.Windows;

namespace Bahnfahrplan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime d;
        IBahnAPI bahnAPI;
        string uri = "http://api.deutschebahn.com/freeplan/v1/";

        public MainWindow()
        {
            InitializeComponent();
        }

        //http://api.deutschebahn.com/freeplan/v1/
        //Lizenz Creative Commons Attribution 4.0 International (CC BY 4.0).
        //by Deutsche Bahn AG
        private void button_GetInformation_Click(object sender, RoutedEventArgs e)
        {
            //Später Datum auswählbar?
            d = DateTime.Now;

            string location = TextBox_StationName.Text;
            if (bahnAPI == null)
            {
                bahnAPI = new BahnAPI(uri, d);
            }
            else
            {
                bahnAPI.Date = d;
            }
            try
            {
                List<Location> locationList = bahnAPI.GetLocation(location);

                //Listbox wird gecleared und vorbereitet die Informationen aufzunehmen
                listBox_Information.Items.Clear();

                foreach (var loc in locationList)
                {
                    listBox_Information.Items.Add(loc);
                }
            }
            catch
            {
                MessageBox.Show(" Fehler: BahnAPI,GetLocation - \n" + uri + "location/" + location);
            }
        }

        private void listBox_Information_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int index = listBox_Information.SelectedIndex;

            listBox_Departures.Items.Clear();

            try
            {
                List<Station> stationList = bahnAPI.GetArrivalDepartureStation(index);

                foreach (var station in stationList)
                {
                    listBox_Departures.Items.Add(station);
                }
            }
            catch
            {
                MessageBox.Show(" Fehler: BahnAPI.GetArrivalDepartureStation - \n" + "Irgendwas lief schief");
            }
        }


        private void button_GetDepartures_Click(object sender, RoutedEventArgs e)
        {
            listBox_Information_MouseDoubleClick(sender, null);
        }
    }
}
