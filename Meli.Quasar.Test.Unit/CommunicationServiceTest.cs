

using AutoMapper;
using FluentAssertions;
using Meli.Quasar.Api.AutoMapper;
using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Common.Enums;
using Meli.Quasar.DataAccess.Interface;
using Meli.Quasar.Domain.Entities;
using Meli.Quasar.Domain.Exceptions;
using Meli.Quasar.Service;
using Meli.Quasar.Service.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Meli.Quasar.Test.Unit
{
    public class CommunicationServiceTest
    {
        //Mock Dependencies

        public Mock<ICommunicationRepository> ICommunicationRepositoryMock;
        public Mock<ILogger<CommunicationService>> ILoggerMock { get; set; }

        public Mock<IMessageService> IMessageServiceMock;

        public Mock<ILocationService> ILocationServiceMock;

        public CommunicationService Sut { get; set; }


        public CommunicationServiceTest()
        {
            var myProfile = new CommunicationProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            ICommunicationRepositoryMock = new Mock<ICommunicationRepository>();
            ILoggerMock = new Mock<ILogger<CommunicationService>>();
            ILocationServiceMock = new Mock<ILocationService>();
            IMessageServiceMock = new Mock<IMessageService>();


            ICommunicationRepositoryMock.Setup(x => x.GetSatellites()).Returns(new List<Satellite> {
                new Satellite(SatelliteNames.kenobi.ToString(), new Point(-500,-200)),
                new Satellite(SatelliteNames.skywalker.ToString(), new Point(100, -100)),
                new Satellite(SatelliteNames.sato.ToString(), new Point(500, 100)),
            });

            Sut = new CommunicationService(ILoggerMock.Object, ICommunicationRepositoryMock.Object, mapper, IMessageServiceMock.Object, ILocationServiceMock.Object);
        }

        [Fact]
        public void When_sends_the_correct_satellite_information_to_TopSecretMethod_it_should_return_a_correct_message_and_location()
         {
            //ARRANGE
            ILocationServiceMock.Setup(x => x.GetLocation(It.IsAny<PointDistance>(), It.IsAny<PointDistance>(), It.IsAny<PointDistance>()))
                .Returns(new PointDto(-487.59, 1574.99));

            IMessageServiceMock.Setup(x => x.GetMessage(It.IsAny<List<List<string>>>())).Returns("este es un mensaje secreto");

            var dtoResquest = new PostTopSecretRequestDto(new List<SatalliteDto>
                {
                    new SatalliteDto(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", ""}),
                    new SatalliteDto(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "es", "", "", "secreto"}),
                    new SatalliteDto(SatelliteNames.sato.ToString(), 142.7, new List<string>{ "este", "", "un", "", ""})
                });
            
           var response = Sut.TopSecret(dtoResquest);

            response.Message.Should().Be("este es un mensaje secreto");
            response.Position.X.Should().Be(-487.59);
            response.Position.Y.Should().Be(1574.99);
        }

        [Fact]
        public void When_sends_the_wrong_message_information_to_TopSecretMethod_it_should_thrown_calculate_message_exception()
        {
            //ARRANGE
            ILocationServiceMock.Setup(x => x.GetLocation(It.IsAny<PointDistance>(), It.IsAny<PointDistance>(), It.IsAny<PointDistance>()))
               .Returns(new PointDto(-487.59, 1574.99));

            IMessageServiceMock.Setup(x => x.GetMessage(It.IsAny<List<List<string>>>())).Throws(new CalculateMessageException());


            var dtoResquest = new PostTopSecretRequestDto(new List<SatalliteDto>
                {
                    new SatalliteDto(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", "", ""}),
                    new SatalliteDto(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "", "", "", "secreto"}),
                    new SatalliteDto(SatelliteNames.sato.ToString(), 142.7, new List<string>{ "este", "", "un", "", ""})
                });

            //ACT
            Action test = () => Sut.TopSecret(dtoResquest);

            //ASSERT
            test.Should().Throw<CalculateMessageException>();
        }

        [Fact]
        public void When_sends_the_wrong_distances_information_to_TopSecretMethod_it_should_thrown_positioncalcuale_message_exception()
        {
            //ARRANGE
            ILocationServiceMock.Setup(x => x.GetLocation(It.IsAny<PointDistance>(), It.IsAny<PointDistance>(), It.IsAny<PointDistance>()))
                .Throws(new CalculatePositionException());

            var dtoResquest = new PostTopSecretRequestDto(new List<SatalliteDto>
                {
                    new SatalliteDto(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", "", ""}),
                    new SatalliteDto(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "", "", "", "secreto"}),
                });

            //ACT
            Action test = () => Sut.TopSecret(dtoResquest);

            //ASSERT
            test.Should().Throw<CalculatePositionException>();
        }


        //Top Secret Split

        [Fact]
        public void When_sends_the_correct_satellite_split_information_to_TopSecretSplit_method_it_should_return_a_correct_message_and_location()
        {
            //ARRANGE
            ICommunicationRepositoryMock.Setup(x => x.GetSatellitesSplit()).Returns(new List<SatelliteSplit> { 
                new SatelliteSplit(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", ""}),
                new SatelliteSplit(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "es", "", "", "secreto"}),
                new SatelliteSplit(SatelliteNames.sato.ToString(), 142.7, new List<string>{ "este", "", "un", "", ""})
            });

            ILocationServiceMock.Setup(x => x.GetLocation(It.IsAny<PointDistance>(), It.IsAny<PointDistance>(), It.IsAny<PointDistance>()))
                .Returns(new PointDto(-487.59, 1574.99));

            IMessageServiceMock.Setup(x => x.GetMessage(It.IsAny<List<List<string>>>())).Returns("este es un mensaje secreto");

            var dtoResquest = new PostTopSecretRequestDto(new List<SatalliteDto>
                {
                    new SatalliteDto(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", ""}),
                    new SatalliteDto(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "es", "", "", "secreto"}),
                    new SatalliteDto(SatelliteNames.sato.ToString(), 142.7, new List<string>{ "este", "", "un", "", ""})
                });

            //ACT
            var response = Sut.TopSecretSplit();

            //ASSERT
            response.Message.Should().Be("este es un mensaje secreto");
            response.Position.X.Should().Be(-487.59);
            response.Position.Y.Should().Be(1574.99);
        }

        [Fact]
        public void When_sends_the_wrong_satellite_split_information_to_TopSecretSplit_method_it_should_return_a_correct_message_and_location()
        {
            //ARRANGE
            ICommunicationRepositoryMock.Setup(x => x.GetSatellitesSplit()).Returns(new List<SatelliteSplit>());

            ILocationServiceMock.Setup(x => x.GetLocation(It.IsAny<PointDistance>(), It.IsAny<PointDistance>(), It.IsAny<PointDistance>()))
                .Throws(new CalculatePositionException());

            //ACT
            Action test = () => Sut.TopSecretSplit();

            //ASSERT
            test.Should().Throw<CalculatePositionException>();
        }
    }
}
