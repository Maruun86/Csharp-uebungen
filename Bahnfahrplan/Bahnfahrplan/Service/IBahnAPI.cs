using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Bahnfahrplan
{
    interface IBahnAPI
    {
        public DateTime Date
        {
            get;
            set;
        }
        /// <summary>
        /// Erzeugt eine Anfrage auf uri /location um einen JSON-String zu erhalten
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns>Gibt eine List <see cref="Location"/> zurück</returns>
        public List<Location> GetLocation(string locationName);

        /// <summary>
        /// Erzeugt eine Anfrage für Arrival-Daten einer bestimmten Station
        /// </summary> 
        /// <param name="i">Ein Index wird hier benötigt</param>
        /// <returns><see cref="JArray"/> wird wiedergegeben mit allen Informationen</returns>
        public JArray GetArrival(int i);

        /// <summary>
        /// Erzeugt eine Anfrage für Departure-Daten einer bestimmten Station
        /// </summary> 
        /// <param name="i">Ein Index wird hier benötigt</param>
        /// <returns><see cref="JArray"/> wird wiedergegeben mit allen Informationen</returns>
        public JArray GetDeparture(int i);

        /// <summary>
        /// Hier werden 2 JArray zu einer Stationsliste zusammengefasst.
        /// </summary>
        /// <param name="jArray1">Erste JArray für den Merge</param>
        /// <param name="jArray2">Zweites JArray für den Merge</param>
        /// <returns> List <see cref="Station"/> wird zurückggeben</returns>
        public List<Station> GetArrivalDepartureStation(int index);
    }
}