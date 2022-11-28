using Microsoft.Extensions.Configuration;

namespace Todo.IntegrationTests.Fixtures;

public class DbFixture : IDisposable
{
    public DbFixture()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("testSettings.json")
            .Build();

        this.DbContextSettings = new MongoDbContextSettings(connString, dbName);
        this.DbContext = new MongoDbContext(this.DbContextSettings);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
