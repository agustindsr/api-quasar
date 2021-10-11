using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Meli.Quasar.Common.Dtos.Communication
{
    public class SatelliteSplitRequestDto
    {
        [Range(0, double.MaxValue, ErrorMessage = "La distancia a un satellite no puede ser negativa")]
        public double Distance { get; }
        public List<string> Message { get; }

        public SatelliteSplitRequestDto(double distance, List<string> message)
        {
            Distance = distance;
            Message = message;
        }
    }
}
