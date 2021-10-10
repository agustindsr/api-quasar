using Meli.Quasar.Test.Infrastructure;
using Xunit;

namespace Meli.Quasar.Test.Integration
{
    [CollectionDefinition(Name)]
    public class ServerFixtureIntegrationCollection : ICollectionFixture<ServerFixture>
    {
        public const string Name = "ServerFixture collection";
    }
}
