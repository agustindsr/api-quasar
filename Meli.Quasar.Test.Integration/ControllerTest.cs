using FluentAssertions;
using Meli.Quasar.Api.Exceptions;
using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Common.Enums;
using Meli.Quasar.Test.Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Meli.Quasar.Test.Integration
{
    [Collection(ServerFixtureIntegrationCollection.Name)]
    public class ControllerTest
    {
        private readonly ServerFixture _server;

        public ControllerTest(ServerFixture server)
        {
            _server = server;
        }

        [Fact]
        public void SwaggerTest()
        {
            var httpResponseMessage = _server
                .HttpServer
                .HttpClient
                .GetAsync("/swagger/v1/swagger.json")
                .ConfigureAwait(false);

            httpResponseMessage.Should().NotBeNull();
        }

        [Fact]
        public async Task topsecret_success_case_Test()
        {
            var dtoResquest = new PostTopSecretRequestDto(new List<SatalliteDto>
                {
                    new SatalliteDto(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", ""}),
                    new SatalliteDto(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "es", "", "", "secreto"}),
                    new SatalliteDto(SatelliteNames.sato.ToString(), 142.7, new List<string>{ "este", "", "un", "", ""})
                });
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(dtoResquest), Encoding.UTF8, "application/json");


            var response = await _server
                .HttpServer
                .HttpClient
                .PostAsync("/topsecret", httpContent);

            string stringResult = await response.Content.ReadAsStringAsync();

            TopSecretResponseDto postTopSecretResponseDto = JsonConvert.DeserializeObject<TopSecretResponseDto>(stringResult);

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            postTopSecretResponseDto.Should().NotBeNull();
            postTopSecretResponseDto.Message.Should().Be("este es un mensaje secreto");
            postTopSecretResponseDto.Position.X.Should().Be(-487.59);
            postTopSecretResponseDto.Position.Y.Should().Be(1574.99);

            response.Should().NotBeNull();
        }

        [Fact]
        public async Task topsecret_fail_case_Test()
        {
            var dtoResquest = new PostTopSecretRequestDto(new List<SatalliteDto>
                {
                    new SatalliteDto(SatelliteNames.kenobi.ToString(), 100.0, new List<string> { "este", "", "", "mensaje", ""}),
                    new SatalliteDto(SatelliteNames.skywalker.ToString(), 115.5, new List<string> { "", "es", "", "", "secreto"}),
                });
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(dtoResquest), Encoding.UTF8, "application/json");


            var response = await _server
                .HttpServer
                .HttpClient
                .PostAsync("/topsecret", httpContent);

            string stringResult = await response.Content.ReadAsStringAsync();

            ErrorDetailModel postTopSecretResponseDto = JsonConvert.DeserializeObject<ErrorDetailModel>(stringResult);

            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            postTopSecretResponseDto.Should().NotBeNull();
            postTopSecretResponseDto.Detail.Should().Be("No se pudo calcular la posición");
        }

        [Fact]
        public async Task topsecret_split_success_case_Test()
        {
            var satoSateliteSplitDto = new PostSatelliteSplitRequestDto(142.7, new List<string> { "este", "", "un", "", "" });
            var kenobiSateliteSplitDto = new PostSatelliteSplitRequestDto(100.0, new List<string> { "este", "", "", "mensaje", "" });
            var skywalkerSateliteSplitDto = new PostSatelliteSplitRequestDto(115.5, new List<string> { "", "es", "", "", "secreto" });

            HttpContent httpContentSato = new StringContent(JsonConvert.SerializeObject(satoSateliteSplitDto), Encoding.UTF8, "application/json");
            HttpContent httpContentKenobi = new StringContent(JsonConvert.SerializeObject(kenobiSateliteSplitDto), Encoding.UTF8, "application/json");
            HttpContent httpContentSkywalker = new StringContent(JsonConvert.SerializeObject(skywalkerSateliteSplitDto), Encoding.UTF8, "application/json");

        
            var responseKenobi = await _server
                .HttpServer
                .HttpClient
                .PostAsync($"/topsecret_split/{SatelliteNames.kenobi}", httpContentKenobi);

            var responseSato = await _server
                .HttpServer
                .HttpClient
                .PostAsync($"/topsecret_split/{SatelliteNames.sato}", httpContentSato);

            var responseSkywalker = await _server
                .HttpServer
                .HttpClient
                .PostAsync($"/topsecret_split/{SatelliteNames.skywalker}", httpContentSkywalker);

            responseKenobi.StatusCode.Should().Be(StatusCodes.Status200OK);
            responseSato.StatusCode.Should().Be(StatusCodes.Status200OK);
            responseSkywalker.StatusCode.Should().Be(StatusCodes.Status200OK);

            var responseTopSecretSplit = await _server
              .HttpServer
              .HttpClient
              .GetAsync("/topsecret_split");

            responseTopSecretSplit.StatusCode.Should().Be(StatusCodes.Status200OK);
            string stringResult = await responseTopSecretSplit.Content.ReadAsStringAsync();

            TopSecretResponseDto postTopSecretResponseDto = JsonConvert.DeserializeObject<TopSecretResponseDto>(stringResult);

            postTopSecretResponseDto.Should().NotBeNull();
            postTopSecretResponseDto.Message.Should().Be("este es un mensaje secreto");
            postTopSecretResponseDto.Position.X.Should().Be(-487.59);
            postTopSecretResponseDto.Position.Y.Should().Be(1574.99);
        }

        [Fact]
        public async Task topsecret_split_fail_case_Test()
        {
            var satoSateliteSplitDto = new PostSatelliteSplitRequestDto(142.7, new List<string> { "este", "", "un", "", "" });
            var kenobiSateliteSplitDto = new PostSatelliteSplitRequestDto(100.0, new List<string> { "este", "", "", "mensaje", "" });

            HttpContent httpContentSato = new StringContent(JsonConvert.SerializeObject(satoSateliteSplitDto), Encoding.UTF8, "application/json");
            HttpContent httpContentKenobi = new StringContent(JsonConvert.SerializeObject(kenobiSateliteSplitDto), Encoding.UTF8, "application/json");


            var responseKenobi = await _server
                .HttpServer
                .HttpClient
                .PostAsync($"/topsecret_split/{SatelliteNames.kenobi}", httpContentKenobi);

            var responseSato = await _server
                .HttpServer
                .HttpClient
                .PostAsync($"/topsecret_split/{SatelliteNames.sato}", httpContentSato);

           

            responseKenobi.StatusCode.Should().Be(StatusCodes.Status200OK);
            responseSato.StatusCode.Should().Be(StatusCodes.Status200OK);

            var responseTopSecretSplit = await _server
              .HttpServer
              .HttpClient
              .GetAsync("/topsecret_split");

            responseTopSecretSplit.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            string stringResult = await responseTopSecretSplit.Content.ReadAsStringAsync();

            ErrorDetailModel postTopSecretResponseDto = JsonConvert.DeserializeObject<ErrorDetailModel>(stringResult);

            responseTopSecretSplit.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            postTopSecretResponseDto.Should().NotBeNull();
            postTopSecretResponseDto.Detail.Should().Be("No se pudo calcular la posición");
        }
    }
}
