using System.Collections.Generic;

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class TopSecretRequestDto
    {
        public List<SatalliteDto> Satellites { get; }
        public TopSecretRequestDto(List<SatalliteDto> satellites)
        {
            Satellites = satellites;
        }
    }
}
