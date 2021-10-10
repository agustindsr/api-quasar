using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meli.Quasar.Api.Exceptions
{
    /// <summary>
    /// IInvalidResponseBuilder
    /// </summary>
    public interface IInvalidResponseBuilder
    {
        /// <summary>
        /// Build
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IActionResult Build(ActionExecutingContext context);
    }
}