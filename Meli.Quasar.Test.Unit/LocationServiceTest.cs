

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
    public class LocationServiceTest
    {
        //Mock Dependencies

        public Mock<ILogger<LocationService>> ILoggerMock { get; set; }

        public LocationService Sut { get; set; }


        public LocationServiceTest()
        {
            var myProfile = new CommunicationProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            ILoggerMock = new Mock<ILogger<LocationService>>();

            Sut = new LocationService(ILoggerMock.Object,  mapper);
        }

        [Fact]
        public void When_sends_the_corrects_points_to_GetLocation_it_should_return_a_correct_position()
         {
            //ARRANGE
            var point1 = new PointDistance(new Point(-500,-200), 100);
            var point2 = new PointDistance(new Point(100,-100), 115.5);
            var point3 = new PointDistance(new Point(500, 100), 142.7);

            //ACT
            var position = Sut.GetLocation(point1, point2, point3);

            //ASSERT
            position.X.Should().Be(-487.58584998651804);
            position.Y.Should().Be(1574.99453560333);
        }


        [Fact]
        public void When_sends_incomplete_points_to_GetLocation_it_should_return_exception()
        {
            //ARRANGE
            var point1 = new PointDistance(new Point(-500, -200), 100);
            var point2 = new PointDistance(new Point(100, -100), 115.5);

            //ACT
            Action test = () => Sut.GetLocation(point1, point2, null);

            //ASSERT
            test.Should().Throw<CalculatePositionException>();
        }
    }
}
