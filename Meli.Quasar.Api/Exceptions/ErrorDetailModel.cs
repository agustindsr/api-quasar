using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meli.Quasar.Api.Exceptions
{
    /// <summary>
    /// Detalle de error model
    /// </summary>
    [JsonObject(Title = "detalle_error_model")]
    public class ErrorDetailModel
    {
        /// <summary>
        /// ErrorDetailModel
        /// </summary>
        public ErrorDetailModel()
        {
            Errors = new List<Error>();
        }
        /// <summary>
        /// Descripción del código de error Http
        /// </summary>
        [JsonProperty(PropertyName = "estado")]
        public string State { get; set; }

        /// <summary>
        /// Codigo HTTP
        /// </summary>
        [JsonProperty(PropertyName = "codigo")]
        public int Code { get; set; }

        /// <summary>
        /// Detalle del error
        /// </summary>
        [JsonProperty(PropertyName = "detalle")]
        public string Detail { get; set; }

        /// <summary>
        /// Lista de errores
        /// </summary>
        [JsonProperty(PropertyName = "errores")]
        public List<Error> Errors { get; set; }
    }

    /// <summary>
    /// Error
    /// </summary>
    [JsonObject(Title = "error")]
    public class Error
    {
       
        /// <summary>
        /// Titulo
        /// </summary>
        [JsonProperty(PropertyName = "titulo")]
        public string Title { get; set; }

        /// <summary>
        /// Origen
        /// </summary>
        [JsonProperty(PropertyName = "origen")]
        public string Source { get; set; }

        /// <summary>
        /// Detalle
        /// </summary>
        [JsonProperty(PropertyName = "detalle")]
        public string Detail { get; set; }

        /// <summary>
        /// TrackId
        /// </summary>
        [JsonProperty(PropertyName = "spvtrack_id")]
        public string SpvTrackId { get; set; }
    }
}
