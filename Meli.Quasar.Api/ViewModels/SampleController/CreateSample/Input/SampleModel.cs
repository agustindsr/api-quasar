using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Meli.Quasar.Api.ViewModels.SampleController.CreateSample.Input
{
    /// <summary>
    /// Sample Model
    /// </summary>
    [JsonObject(Title = "SampleModelResponse")]
    public class SampleModel
    {
        /// <summary>
        /// Descripción
        /// </summary>
        [JsonProperty(PropertyName = "descripcion")]
        [Required(ErrorMessage = "El campo descripcion es obligatorio.")]
        [StringLength(5, ErrorMessage = "El campo descripción, permite una longitud maxima de {1} caracteres. ")]
        public string Description { get; set; }
    }
}
