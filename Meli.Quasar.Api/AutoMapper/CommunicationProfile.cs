using AutoMapper;
using Meli.Quasar.Domain.Entities;

namespace Meli.Quasar.Api.AutoMapper
{
    /// <summary>
    /// Used to register classes to map
    /// </summary>
    public class CommunicationProfile: Profile
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public CommunicationProfile()
        {
           CreateMap<Point, Common.Dtos.Communication.PointDto>().ReverseMap();
            CreateMap<SatelliteSplit, Common.Dtos.Communication.SatelliteSplitResponseDto>().ReverseMap();

        }
    }
}
