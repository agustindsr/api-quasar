using Newtonsoft.Json;

namespace Meli.Quasar.Api.ViewModels.SampleController.CreateSample.Output
{
    /// <summary>
    /// Sample Model Response
    /// </summary>
    [JsonObject(Title = "SampleModelResponse")]
    public class SampleModelResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Descripción
        /// </summary>
        [JsonProperty(PropertyName = "descripcion")]
        public string Description { get; set; }
    }
}
