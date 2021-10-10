using System;
using System.Collections.Generic;
using System.Text;

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class PostSatelliteSplitRequestDto
    {
        public double Distance { get; }
        public List<string> Message { get; }

        public PostSatelliteSplitRequestDto(double distance, List<string> message)
        {
            Distance = distance;
            Message = message;
        }
    }
}
