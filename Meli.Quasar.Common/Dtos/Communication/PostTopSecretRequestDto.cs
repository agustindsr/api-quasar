using System.Collections.Generic;

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class PostTopSecretRequestDto
    {
        public List<SatalliteDto> Satellites { get; }
        public PostTopSecretRequestDto(List<SatalliteDto> satellites)
        {
            Satellites = satellites;
        }
    }
}
