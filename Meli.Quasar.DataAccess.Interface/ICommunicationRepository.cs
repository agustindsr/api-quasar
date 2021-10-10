using Meli.Quasar.Domain.Entities;
using System.Collections.Generic;

namespace Meli.Quasar.DataAccess.Interface
{
    public interface ICommunicationRepository
    {
        public List<Satellite> GetSatellites();

        List<SatelliteSplit> GetSatellitesSplit();

        void AddOrUpdateSatelliteSplit(SatelliteSplit satelliteDistance);

        void DeleteSatelliteSplits(string name);
    }
}
