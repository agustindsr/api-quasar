using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Service.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Meli.Quasar.Api.Exceptions;
using Meli.Quasar.Common.Attributes;

namespace Meli.Quasar.Api.Controllers
{
    /// <summary>
    /// CommunicationController
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    public class CommunicationController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        /// <summary>
        /// CommunicationController
        /// </summary>
        /// <param name="communicationService"></param>
        public CommunicationController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        /// <summary>
        /// Communication top secret
        /// </summary>
        /// <returns></returns>
        [HttpPost("topsecret")]
        [SwaggerOperation(Summary = "Calcula las coordenadas del emisor del mensaje y determina el mensaje enviado", Tags = new[] {"Communication"})]
        [ProducesResponseType(typeof(TopSecretResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailModel), StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, "application/problem+json")]
        public ActionResult<TopSecretResponseDto> TopSecret([Required][FromBody] TopSecretRequestDto topSecretRequestDto)
        {

            var response = _communicationService.TopSecret(topSecretRequestDto);
            return Ok(response);
        }

        /// <summary>
        /// Communication top secret
        /// </summary>
        /// <returns></returns>
        [HttpPost("topsecret_split/{satellite_name}")]
        [SwaggerOperation(Summary = "Almacena los datos de distancia y mensaje del satelite para poder usarlos luego", Tags = new[] { "Split Communication" })]
        [ProducesResponseType(typeof(SatelliteSplitRequestDto), StatusCodes.Status201Created)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, "application/problem+json")]
        public ActionResult<SatelliteSplitResponseDto> AddOrUpdateSatelliteSplit([SatelliteName][FromRoute(Name = "satellite_name")] string satelliteName, 
            [Required][FromBody] SatelliteSplitRequestDto postSatelliteSplitRequestDto)
        {
           var response = _communicationService.AddOrUpdateSatelliteSplit(satelliteName, postSatelliteSplitRequestDto);
            return Created("", response);
        }


        /// <summary>
        /// Communication top secret
        /// </summary>
        /// <returns></returns>
        [HttpDelete("topsecret_split/{satellite_name}")]
        [SwaggerOperation(Summary = "Elimina un satelite almacenado.", Tags = new[] { "Split Communication" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDetailModel), StatusCodes.Status409Conflict)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, "application/problem+json")]
        public ActionResult DeleteSatelliteSplit([Required][SatelliteName][FromRoute(Name = "satellite_name")] string satelliteName)
        {
            _communicationService.DeleteSatelliteSplits(satelliteName);
            return NoContent();
        }

        /// <summary>
        /// Communication top secret
        /// </summary>
        /// <returns></returns>
        [HttpGet("topsecret_split")]
        [SwaggerOperation(Summary = "Retorna la posicion y el mensaje si es posible determinarlos con los datos almacenados", Tags = new[] { "Split Communication" })]
        [ProducesResponseType(typeof(TopSecretResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailModel), StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, "application/problem+json")]
        public ActionResult<TopSecretResponseDto> TopSecretSplit()
        {
            var response = _communicationService.TopSecretSplit();

            return Ok(response);
        }

        /// <summary>
        /// Communication top secret
        /// </summary>
        /// <returns></returns>
        [HttpGet("satellites_slipts")]
        [SwaggerOperation(Summary = "Retorna la lista de los datos almacenados", Tags = new[] { "Split Communication" })]
        [ProducesResponseType(typeof(List<SatelliteSplitResponseDto>), StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, "application/problem+json")]
        public ActionResult<List<SatelliteSplitResponseDto>> GetSatellitesSlipts()
        {
            var response = _communicationService.GetSatellitesSplits();

            return Ok(response);
        }
    }
}
