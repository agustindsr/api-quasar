using System;

namespace Meli.Quasar.Domain.Entities
{
    public class Satellite
    {
        public string Name { get; set; }
        public Point Position { get; set; }

        public Satellite(string name, Point position) {
            Name = name;
            Position = position;
        }

        public override bool Equals(Object obj)
        {
            Satellite satellite = obj as Satellite;
            if (satellite == null)
                return false;
            else
                return base.Equals((Satellite)obj) && Name == satellite.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
