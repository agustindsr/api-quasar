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

        public void DeleteSatelliteSplits(string name)
        {
            if (ExistSatelliteSplit(name))
            {
                SatellitesSplit.Remove(SatellitesSplit.Find(x => x.Name == name));
            }
        }

        public SatelliteSplit AddSatelliteSplit(SatelliteSplit satelliteSplit)
        {
            var satelliteSplitCreated = new SatelliteSplit(satelliteSplit.Name, satelliteSplit.Distance, satelliteSplit.Message);
            SatellitesSplit.Add(satelliteSplitCreated);
            return satelliteSplitCreated;
        }

        public SatelliteSplit UpdateSatelliteSplit(SatelliteSplit satelliteSplit)
        {
            var satellite = GetSatelliteSplitByName(satelliteSplit.Name);
            satellite = satelliteSplit;
            return satellite;
        }

        public bool ExistSatelliteSplit(string name)
        {
            return SatellitesSplit.Any(x => x.Name == name);
        }
        private SatelliteSplit GetSatelliteSplitByName(string name)
        {
            return SatellitesSplit.Find(x => x.Name == name);
        }
    }
}
