using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Common.LogEvents;
using Meli.Quasar.DataAccess.Interface;
using Meli.Quasar.Service.Interface;
using System.Linq;
using Meli.Quasar.Domain.Entities;
using Newtonsoft.Json;

namespace Meli.Quasar.Service
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ILogger<CommunicationService> _logger;
        private readonly ICommunicationRepository _communicationRepository;
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly IMessageService _messageService;


        public CommunicationService(ILogger<CommunicationService> logger, ICommunicationRepository communicationRepository, IMapper mapper,
            IMessageService messageService, ILocationService locationService)
        {
            _logger = logger;
            _communicationRepository = communicationRepository;
            _mapper = mapper;
            _messageService = messageService;
            _locationService = locationService;
        }


        #region Public methods
        public TopSecretResponseDto TopSecret(PostTopSecretRequestDto postTopSecretResquestDto)
        {
            try
            {
                _logger.LogInformation(ServiceEvents.CallingTopSecret, ServiceEvents.CallingTopSecret.Name,
                                        JsonConvert.SerializeObject(postTopSecretResquestDto));

                var satellites = _communicationRepository.GetSatellites();

                var points = new PointDistance[3];

                for (int i = 0; i < postTopSecretResquestDto.Satellites.Count; i++)
                {
                    var satellite = satellites.Find(x => x.Name == postTopSecretResquestDto.Satellites[i].Name);
                    points[i] = new PointDistance(satellite.Position, postTopSecretResquestDto.Satellites[i].Distance);
                }

                PointDto position = _locationService.GetLocation(points[0], points[1], points[2]);
                string message = _messageService.GetMessage(postTopSecretResquestDto.Satellites.Select(x => x.Message).ToList());

                return new TopSecretResponseDto(position, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ServiceEvents.ExceptionCallingTopSecret, ex.Message, ex);
                throw;
            }
        }

        public TopSecretResponseDto TopSecretSplit()
        {
            try
            {
                _logger.LogInformation(ServiceEvents.CallingTopSecretSplit, ServiceEvents.CallingTopSecretSplit.Name);

                var satellites = _communicationRepository.GetSatellites();
                var satellitesSplits = _communicationRepository.GetSatellitesSplit();

                var points = new PointDistance[3];

                for (int i = 0; i < satellitesSplits.Count; i++)
                {
                    var satellite = satellites.Find(x => x.Name == satellitesSplits[i].Name);
                    points[i] = new PointDistance(satellite.Position, satellitesSplits[i].Distance);
                }

                PointDto position = _locationService.GetLocation(points[0], points[1], points[2]);
                string message = _messageService.GetMessage(satellitesSplits.Select(x => x.Message).ToList());

                return new TopSecretResponseDto(position, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ServiceEvents.ExceptionCallingTopSecretSplit, ex.Message, ex);

                throw;
            }
        }

        public List<SatelliteSplitDto> GetSatellitesSplits()
        {
            _logger.LogInformation(ServiceEvents.CallingGetSatellitesSplits, ServiceEvents.CallingGetSatellitesSplits.Name);

            return _mapper.Map<List<SatelliteSplitDto>>(_communicationRepository.GetSatellitesSplit());
        }

        public void DeleteSatelliteSplits(string name)
        {
            _logger.LogInformation(ServiceEvents.CallingDeleteSatelliteSplits, ServiceEvents.CallingDeleteSatelliteSplits.Name);

            _communicationRepository.DeleteSatelliteSplits(name);
        }

        public void AddOrUpdateSatelliteSplit(string name, PostSatelliteSplitRequestDto postSatelliteSplitRequestDto)
        {
            _logger.LogInformation(ServiceEvents.CallingAddOrUpdateSatelliteSplit, ServiceEvents.CallingAddOrUpdateSatelliteSplit.Name);

            var sateliteSplit = new SatelliteSplit(name, postSatelliteSplitRequestDto.Distance, postSatelliteSplitRequestDto.Message);

            _communicationRepository.AddOrUpdateSatelliteSplit(sateliteSplit);
        }
        #endregion

    }
}
