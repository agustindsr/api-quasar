using System;
using System.Diagnostics;

namespace Meli.Quasar.Test.Infrastructure
{
    public class ServerFixture : IDisposable
    {
        public ServerMock HttpServer { get; }

        public ServerFixture()
        {
            Debug.Write("ServerFixture Constructor - Se ejecuta una sola vez antes de la ejecución de los test.");

            HttpServer = new ServerMock();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
