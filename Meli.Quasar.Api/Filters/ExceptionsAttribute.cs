using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Meli.Quasar.Api.Exceptions;
using Meli.Quasar.Domain.Exceptions;

namespace Meli.Quasar.Api.Filters
{
    /// <summary>
    /// ExceptionsAttribute
    /// </summary>
    public class ExceptionsAttribute : Attribute, IExceptionFilter
    {
        //private const string BusinessException = "BusinessException";
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnException(ExceptionContext context)
        {

            var errorDetail = new ErrorDetailModel
            {
                State = ((HttpStatusCode) SetErrorCode(context)).ToString(),
                Code = SetErrorCode(context)
            };
            errorDetail.Detail = context.Exception.Message;
            errorDetail.Errors.Add(new Error
            {
                Title = context.Exception.Message,
                Detail = context.Exception.InnerException?.Message ?? string.Empty,
                Source = context.Exception.Source,
                SpvTrackId = context.HttpContext.TraceIdentifier
            });

            context.Result = new ObjectResult(errorDetail);
            context.HttpContext.Response.StatusCode = SetErrorCode(context);
        }


        private int SetErrorCode(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();

            switch (exceptionType.Name)
            {
                case nameof(CalculateMessageException):
                {
                    var data = (CalculateMessageException) context.Exception;

                        return data.Code;
                }

                case nameof(CalculatePositionException):
                    {
                        var data = (CalculatePositionException)context.Exception;

                        return data.Code;
                    }

                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
