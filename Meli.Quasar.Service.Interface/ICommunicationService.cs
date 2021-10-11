using Meli.Quasar.Common.Dtos.Communication;
using System.Collections.Generic;

namespace Meli.Quasar.Service.Interface
{
    public interface ICommunicationService
    {
        TopSecretResponseDto TopSecret(TopSecretRequestDto topSecretRequestDto);

        List<SatelliteSplitResponseDto> GetSatellitesSplits();

        SatelliteSplitResponseDto AddOrUpdateSatelliteSplit(string name, SatelliteSplitRequestDto postSatelliteSplitRequestDto);

        TopSecretResponseDto TopSecretSplit();

        void DeleteSatelliteSplits(string name);
     }
}
