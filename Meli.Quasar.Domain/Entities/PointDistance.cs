

namespace Meli.Quasar.Domain.Entities
{
    public class PointDistance : Point
    {
        public double Distance { get; }
        public PointDistance(Point point, double distance) : base(point.X, point.Y)
        {
            Distance = distance;
        }
        public PointDistance(double latitud, double longitud, double distance) : base(latitud, longitud) {
            Distance = distance;
        }
    }
}
