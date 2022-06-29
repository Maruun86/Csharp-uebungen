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
        public List<Location> GetLocation(string locationName);
        public JArray GetArrival(int i);
        public JArray GetDeparture(int i);
        public List<Station> GetArrivalDepartureStation(int index);
        public void Setup();
    }
}