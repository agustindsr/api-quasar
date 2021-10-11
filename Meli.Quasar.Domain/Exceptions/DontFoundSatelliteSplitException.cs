using Microsoft.AspNetCore.Http;
using System;

namespace Meli.Quasar.Domain.Exceptions
{
    /// <summary>
    /// Main class for Business Exception builders
    /// </summary>
    public class DontFoundSatelliteSplitException : Exception
    {
        public int Code { get; set; } = StatusCodes.Status409Conflict;

        public DontFoundSatelliteSplitException(string satelliteName) : base($"No existe un satellite de nombre {satelliteName}") { }
    }
}
