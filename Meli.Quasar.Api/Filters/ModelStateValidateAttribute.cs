﻿using Microsoft.AspNetCore.Mvc.Filters;
using Meli.Quasar.Api.Exceptions;

namespace Meli.Quasar.Api.Filters
{
    /// <summary>
    /// ModelStateValidateAttribute
    /// </summary>
    public class ModelStateValidateAttribute : ActionFilterAttribute
    {
        private readonly IInvalidResponseBuilder _invalidResponseBuilder;

        /// <summary>
        /// ModelStateValidateAttribute
        /// </summary>
        /// <param name="invalidResponseBuilder"></param>
        public ModelStateValidateAttribute(IInvalidResponseBuilder invalidResponseBuilder)
        {
            _invalidResponseBuilder = invalidResponseBuilder;
        }

        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = _invalidResponseBuilder.Build(context);
            }
        }
    }
}
