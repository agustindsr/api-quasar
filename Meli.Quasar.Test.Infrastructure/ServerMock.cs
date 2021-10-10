using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Meli.Quasar.Api;

namespace Meli.Quasar.Test.Infrastructure
{
    public class ServerMock : IDisposable
    {
        public HttpClient HttpClient { get; }
        public TestServer TestServer { get; }
        public IWebHostBuilder WebHostBuilder { get; }
        public string UrlHostMock { get; set; }
        public string PortMock { get; set; }
        public IConfigurationRoot Configuration { get; private set; }

        public ServerMock()
        {
            WebHostBuilder = CreateWebHostBuilder();
            TestServer = new TestServer(WebHostBuilder);
            HttpClient = TestServer.CreateClient();
        }

        private IWebHostBuilder CreateWebHostBuilder()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var hostBuilder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>();

            FillConfiguration();

            return hostBuilder;
        }

        private void FillConfiguration()
        {
            PortMock = Configuration.GetValue<string>("PortMock");
            UrlHostMock = Configuration.GetValue<string>("UrlHostMock");
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
