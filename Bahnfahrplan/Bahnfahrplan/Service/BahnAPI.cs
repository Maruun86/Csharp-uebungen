using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Bahnfahrplan
{
    class BahnAPI : IBahnAPI
    {
        WebClient w;
        string uri;
        DateTime date;
        string dateString = "";
        List<Location> locations = new List<Location>();


        /// <summary>
        /// Verwaltende Klasse für API-Handling der Deutschen Bahn
        /// </summary>
        /// <param name="uri">API-URL</param>
        /// <param name="date">Datum das bei Suchanfragen genutzt werden soll</param>
        public BahnAPI(string uri, DateTime date)
        {
            w = new WebClient();
            w.Encoding = Encoding.UTF8;
            this.uri = uri;
            this.date = date;
            Setup();


        }
        /// <summary>
        /// Erzeugt eine Anfrage auf uri /location um einen JSON-String zu erhalten
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns>Gibt eine List <see cref="Location"/> zurück</returns>
        public List<Location> GetLocation(string locationName)
        {
            locationName = WebUtility.UrlEncode(locationName);

            string s = w.DownloadString(uri + "location/" + locationName);
            JArray jArray = JArray.Parse(s);
            List<Location> locationList = new List<Location>();

            foreach (JObject jObject in jArray)
            {
                Location loc = new Location();
                loc.name = (string)jObject["name"];
                loc.id = (string)jObject["id"];
                locationList.Add(loc);
                locations.Add(loc);
            }
            return locationList;
        }

        /// <summary>
        /// Erzeugt eine Anfrage für Arrival-Daten einer bestimmten Station
        /// </summary> 
        /// <param name="i">Ein Index wird hier benötigt</param>
        /// <returns><see cref="JArray"/> wird iwedergegeben mit allen Informationen</returns>
        public JArray GetArrival(int i)
        {
            string id = (string)locations[i].id;
            string s = w.DownloadString(uri + "arrivalBoard/" + id + "?" + dateString);
            JArray jArray = JArray.Parse(s);
            return jArray;
        }
        /// <summary>
        /// Erzeugt eine Anfrage für Departure-Daten einer bestimmten Station
        /// </summary> 
        /// <param name="i">Ein Index wird hier benötigt</param>
        /// <returns><see cref="JArray"/> wird wiedergegeben mit allen Informationen</returns>
        public JArray GetDeparture(int i)
        {
            string id = (string)locations[i].id;
            string s = w.DownloadString(uri + "departureBoard/" + id + "?" + dateString);
            JArray jArray = JArray.Parse(s);
            return jArray;
        }
        /// <summary>
        /// Hier werden 2 JArray zu einer Stationsliste zusammengefasst.
        /// </summary>
        /// <param name="jArray1">Erste JArray für den Merge</param>
        /// <param name="jArray2">Zweites JArray für den Merge</param>
        /// <returns> List <see cref="Station"/> wird zurückggeben</returns>
        public List<Station> GetArrivalDepartureStation(int index)
        {
            JArray jArray1 = GetArrival(index);
            JArray jArray2 = GetDeparture(index);

            List<Station> stationList = new List<Station>();

            for (int i = 0; i < jArray1.Count; i++)
            {
                Station station = new Station();
                station.name = (string)jArray1[i]["name"];
                station.type = (string)jArray1[i]["type"];
                station.dateTimeDeparture = (string)jArray2[i]["dateTime"];
                station.dateTimeArrival = (string)jArray1[i]["dateTime"];
                station.origin = (string)jArray1[i]["origin"];
                station.track = (string)jArray1[i]["track"];
                stationList.Add(station);
            }

            return stationList;
        }
        /// <summary>
        /// Formatiert den DateTime lesbar.
        /// </summary>
        public void Setup()
        {
            string year = NumberWithZero(date.Year);
            string month = NumberWithZero(date.Month);
            string day = NumberWithZero(date.Day);
            string hour = NumberWithZero(date.Hour);
            string minut = NumberWithZero(date.Minute);

            dateString = "date=" + year + "-" + month + "-" + day + "T" + hour + ":" + minut;
        }
        /// <summary>
        /// Erzeugt einen String mit einer 0 Vorweg bei zahlen kleiner als 10
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public string NumberWithZero(int d)
        {
            if (d < 10)
            {

                return "0" + d;
            }
            else
                return d.ToString();
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
