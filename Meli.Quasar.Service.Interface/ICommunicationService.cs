using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Common.Enums;
using Meli.Quasar.Domain.Entities;
using System.Collections.Generic;

namespace Meli.Quasar.Service.Interface
{
    public interface ICommunicationService
    {
        TopSecretResponseDto TopSecret(PostTopSecretRequestDto topSecretRequestDto);

        List<SatelliteSplitDto> GetSatellitesSplits();

        void AddOrUpdateSatelliteSplit(string name, PostSatelliteSplitRequestDto postSatelliteSplitRequestDto);

        TopSecretResponseDto TopSecretSplit();

        void DeleteSatelliteSplits(string name);
     }
}
