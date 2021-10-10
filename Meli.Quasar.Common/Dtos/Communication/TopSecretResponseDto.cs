

namespace Meli.Quasar.Common.Dtos.Communication
{
    public class TopSecretResponseDto
    {
        public string Message { get;  }
        public PointDto Position { get; }

        public TopSecretResponseDto(PointDto position, string message)
        {
            Position = position;
            Message = message;
        }
    }
}
