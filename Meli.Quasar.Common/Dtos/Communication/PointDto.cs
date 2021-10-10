
namespace Meli.Quasar.Common.Dtos.Communication
{
    public class PointDto
    {
        public double X { get; }
        public double Y { get; }

        public PointDto(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
