using Meli.Quasar.Domain.Entities;
using System.Collections.Generic;

namespace Meli.Quasar.DataAccess.Interface
{
    public interface ICommunicationRepository
    {
        public List<Satellite> GetSatellites();

        List<SatelliteSplit> GetSatellitesSplit();

        SatelliteSplit AddSatelliteSplit(SatelliteSplit satelliteDistance);

        SatelliteSplit UpdateSatelliteSplit(SatelliteSplit satelliteSplit);

        void DeleteSatelliteSplits(string name);

        bool ExistSatelliteSplit(string name);
    }
}
