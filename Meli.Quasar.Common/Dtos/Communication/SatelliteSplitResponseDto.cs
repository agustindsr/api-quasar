using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class SatelliteSplitResponseDto
    {
        public string Name { get;}

        [Range(0, double.MaxValue, ErrorMessage = "La distancia a un satellite no puede ser negativa")]
        public double Distance { get; }

        public List<string> Message { get;}

        public SatelliteSplitResponseDto(string name, double distance, List<string> message)
        {
            Name = name;
            Distance = distance;
            Message = message;
        }
    }
}
