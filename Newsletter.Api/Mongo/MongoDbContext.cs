using MongoDB.Driver;
using Newsletter.Api.Entities;

namespace Newsletter.Api.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Subscriber> Subscribers => _database.GetCollection<Subscriber>("Subscribers");
    }
}
