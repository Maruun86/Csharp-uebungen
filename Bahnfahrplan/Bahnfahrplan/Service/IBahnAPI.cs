using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Bahnfahrplan
{
    interface IBahnAPI
    {
        public List<Location> GetLocation(string locationName);
        public JArray GetArrival(int i);
        public JArray GetDeparture(int i);
        public void Setup();
    }
}