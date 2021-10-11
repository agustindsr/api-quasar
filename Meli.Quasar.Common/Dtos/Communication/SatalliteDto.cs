using Meli.Quasar.Common.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class SatalliteDto
    {

        [SatelliteName]
        public string Name { get; }

        [Range(0, double.MaxValue, ErrorMessage = "La distancia a un satellite no puede ser negativa")]
        public double Distance { get;  }
        public List<string> Message { get; }

        public SatalliteDto(string name, double distance, List<string> message)
        {
            Name = name;
            Distance = distance;
            Message = message;
        }
    }
}
