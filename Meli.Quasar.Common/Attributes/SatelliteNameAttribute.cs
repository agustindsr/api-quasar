using Meli.Quasar.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Meli.Quasar.Common.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class SatelliteNameAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Por favor revise los nombre de los sátelites. Los nombres validos son: {string.Join(", ", Enum.GetNames(typeof(SatelliteNames)))}";

        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var name = (string)value;

            if (!Enum.IsDefined(typeof(SatelliteNames), name))
            {
                return new ValidationResult(GetErrorMessage());

            }

            return ValidationResult.Success;
        }
    }
}
