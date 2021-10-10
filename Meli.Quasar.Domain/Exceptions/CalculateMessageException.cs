using Microsoft.AspNetCore.Http;
using System;

namespace Meli.Quasar.Domain.Exceptions
{
    /// <summary>
    /// Main class for Business Exception builders
    /// </summary>
    public class CalculateMessageException : Exception
    {
        public int Code { get; set; } = StatusCodes.Status404NotFound;

        public CalculateMessageException() : base("No se pudo calcular el mensaje") { }
    }
}
