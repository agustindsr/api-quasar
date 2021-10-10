using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;

namespace Meli.Quasar.Domain.Exceptions
{
    /// <summary>
    /// Main class for Business Exception builders
    /// </summary>
    public class BusinessException : Exception
    {
        public string Code { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public BusinessException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public BusinessException(EventId eventId) : base(eventId.Name)
        {
            Code = eventId.Id.ToString();
            Errors = new Dictionary<string, string[]> {{Code, new[] {eventId.Name}}};
        }
    }
}
