using System.Collections.Generic;

namespace Meli.Quasar.Domain.Entities
{
    public class SatelliteSplit
    {
        public string Name { get; }
        public double Distance { get; set; }
        public List<string> Message { get; set; }

        public SatelliteSplit(string name, double distance, List<string> message)
        {
            Name = name;
            Distance = distance;
            Message = message;
        }
    }
}
