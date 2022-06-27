using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace Bahnfahrplan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //http://api.deutschebahn.com/freeplan/v1/location/
        //Lizenz Creative Commons Attribution 4.0 International (CC BY 4.0).
        //by Deutsche Bahn AG
        private void button_GetInformation_Click(object sender, RoutedEventArgs e)
        {
            listBox_Information.Items.Clear();
            WebClient w = new WebClient();
            w.Encoding = Encoding.UTF8;
            string input = WebUtility.UrlEncode(TextBox_StationName.Text);

            string s = w.DownloadString("http://api.deutschebahn.com/freeplan/v1/location/" + input);
            JArray jArray = JArray.Parse(s);

            foreach (JObject jObject in jArray)
            {
                Station station = new Station();
                station.name = (string)jObject["name"];
                station.id = (string)jObject["id"];
                listBox_Information.Items.Add(station);
            }
        }

        private void listBox_Information_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WebClient w = new WebClient();
            w.Encoding = Encoding.UTF8;
            Station station = (Station)listBox_Information.Items[listBox_Information.SelectedIndex];
            string input = WebUtility.UrlEncode(station.id);

            string s = w.DownloadString("http://api.deutschebahn.com/freeplan/v1/arrivalBoard/" + input);
            JArray jArray = JArray.Parse(s);

            foreach (JObject jObject in jArray)
            {

            }
        }
    }
}
