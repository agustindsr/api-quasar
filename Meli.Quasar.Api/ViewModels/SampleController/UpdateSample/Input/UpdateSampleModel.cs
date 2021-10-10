using Newtonsoft.Json;

namespace Meli.Quasar.Api.ViewModels.SampleController.UpdateSample.Input
{
    /// <summary>
    /// Sample Model
    /// </summary>
    [JsonObject(Title = "update-sample-model")]
    public class UpdateSampleModel
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
