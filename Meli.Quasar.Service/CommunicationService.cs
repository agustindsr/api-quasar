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
using Meli.Quasar.Domain.Exceptions;

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


        public TopSecretResponseDto TopSecret(TopSecretRequestDto postTopSecretResquestDto)
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
                
                var response = new TopSecretResponseDto(position, message);

                _logger.LogInformation(ServiceEvents.ReponseTopSecret, ServiceEvents.ReponseTopSecret.Name,
                                           JsonConvert.SerializeObject(response));

                return response;
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


                var response = new TopSecretResponseDto(position, message);

                _logger.LogInformation(ServiceEvents.ReponseTopSecretSplit, ServiceEvents.ReponseTopSecretSplit.Name,
                                           JsonConvert.SerializeObject(response));

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ServiceEvents.ExceptionCallingTopSecretSplit, ex.Message, ex);

                throw;
            }
        }

        public List<SatelliteSplitResponseDto> GetSatellitesSplits()
        {
            _logger.LogInformation(ServiceEvents.CallingGetSatellitesSplits, ServiceEvents.CallingGetSatellitesSplits.Name);

            var response = _mapper.Map<List<SatelliteSplitResponseDto>>(_communicationRepository.GetSatellitesSplit());

            _logger.LogInformation(ServiceEvents.ReponseGetSatellitesSplits, ServiceEvents.ReponseGetSatellitesSplits.Name,
                                          JsonConvert.SerializeObject(response));
            return response;
        }

        public void DeleteSatelliteSplits(string name)
        {
            _logger.LogInformation(ServiceEvents.CallingDeleteSatelliteSplits, ServiceEvents.CallingDeleteSatelliteSplits.Name);

            if (!_communicationRepository.ExistSatelliteSplit(name)) {
                throw new DontFoundSatelliteSplitException(name);
            }

            _communicationRepository.DeleteSatelliteSplits(name);
        }

        public SatelliteSplitResponseDto AddOrUpdateSatelliteSplit(string name, SatelliteSplitRequestDto postSatelliteSplitRequestDto)
        {
            _logger.LogInformation(ServiceEvents.CallingAddOrUpdateSatelliteSplit, ServiceEvents.CallingAddOrUpdateSatelliteSplit.Name);

            var sateliteSplit = new SatelliteSplit(name, postSatelliteSplitRequestDto.Distance, postSatelliteSplitRequestDto.Message);

            if (_communicationRepository.ExistSatelliteSplit(name))
            {
                sateliteSplit = _communicationRepository.UpdateSatelliteSplit(sateliteSplit);
            }
            else
            {
                sateliteSplit = _communicationRepository.AddSatelliteSplit(sateliteSplit);
            }

            var response = _mapper.Map<SatelliteSplitResponseDto>(sateliteSplit);

            _logger.LogInformation(ServiceEvents.ReponseAddOrUpdateSatelliteSplit, ServiceEvents.ReponseAddOrUpdateSatelliteSplit.Name,
                                          JsonConvert.SerializeObject(response));

            return response;
        }
    }
}
