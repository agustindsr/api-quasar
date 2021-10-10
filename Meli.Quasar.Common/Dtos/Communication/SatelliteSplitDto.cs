using System.Collections.Generic;

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class SatelliteSplitDto
    {
        public string Name { get;}
        public double Distance { get; }
        public List<string> Message { get;}

        public SatelliteSplitDto(string name, double distance, List<string> message)
        {
            Name = name;
            Distance = distance;
            Message = message;
        }
    }
}
