using Meli.Quasar.Common.Enums;
using Meli.Quasar.DataAccess.Interface;
using Meli.Quasar.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Meli.Quasar.Repository
{
    public class CommunicationRepository : ICommunicationRepository
    {

        private static List<SatelliteSplit> SatellitesSplit { get; set; } = new List<SatelliteSplit>();
        private static List<Satellite> Satellites { get; } = new List<Satellite> {
                new Satellite(SatelliteNames.kenobi.ToString(), new Point(-500,-200)),
                new Satellite(SatelliteNames.skywalker.ToString(), new Point(100, -100)),
                new Satellite(SatelliteNames.sato.ToString(), new Point(500, 100)),
            };

        public List<Satellite> GetSatellites()
        {
            return Satellites;
        }

        public List<SatelliteSplit> GetSatellitesSplit()
        {
            return SatellitesSplit;
        }

        public void DeleteSatelliteSplits(string name) {
            if (ExistSatelliteSplit(name))
            {
                SatellitesSplit.Remove(SatellitesSplit.Find(x => x.Name == name));
            }
         }

        public void AddOrUpdateSatelliteSplit(SatelliteSplit satelliteSplit)
        {
            if (ExistSatelliteSplit(satelliteSplit.Name))
            {
                var satellite = GetSatelliteByName(satelliteSplit.Name);
                satellite.Distance = satelliteSplit.Distance;
                satellite.Message = satelliteSplit.Message;
            }
            else {
                SatellitesSplit.Add(new SatelliteSplit(satelliteSplit.Name, satelliteSplit.Distance, satelliteSplit.Message));
            }
        }

        private SatelliteSplit GetSatelliteByName(string name)
        {
            return SatellitesSplit.Find(x => x.Name == name);
        }
        private bool ExistSatelliteSplit(string name) {
            return (SatellitesSplit.Any(x => x.Name == name));
        }
    }
}
