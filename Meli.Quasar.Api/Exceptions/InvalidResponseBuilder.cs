﻿using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meli.Quasar.Api.Exceptions
{
    /// <summary>
    /// InvalidResponseBuilder
    /// </summary>
    public class InvalidResponseBuilder : IInvalidResponseBuilder
    {
        /// <summary>
        /// Build
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult Build(ActionExecutingContext context)
        {
            var response = new ErrorDetailModel
            {
                State = HttpStatusCode.BadRequest.ToString(),
                Code = (int)HttpStatusCode.BadRequest,
                Detail = "Se produjeron uno o más errores de validación."
            };
            foreach (var error in context.ModelState.SelectMany(item => item.Value.Errors))
            {
                response.Errors.Add(new Error
                {
                    Title = "El valor enviado es invalido",
                    Detail = error.ErrorMessage,
                    Source = context.ActionDescriptor.DisplayName,
                    SpvTrackId = context.HttpContext.TraceIdentifier
                });
            }

            return new BadRequestObjectResult(response);
        }
    }
}
