using MongoDB.Driver;
using Shared.Configurations;
using User.Api.Entities;
using User.Grpc.Helpers;

namespace User.Api.Persistence
{
    public class UserDbSeed
    {
        public async Task SeedDataAsync(IMongoClient mongoClient, DatabaseSettings settings)
        {
            var databaseName = settings.DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);
            var inventoryCollection = database.GetCollection<UserEntry>("UserEntries");
            if (await inventoryCollection.EstimatedDocumentCountAsync() == 0)
            {
                await inventoryCollection.InsertManyAsync(GetPreconfiguredInventoryEntries());
            }
        }

        private IEnumerable<UserEntry> GetPreconfiguredInventoryEntries()
        {
            return new List<UserEntry>()
            {
                new UserEntry()
                {
                    UserName = "lupin",
                    Password = Helper.CreateMD5("lupin"),
                    SubjectId = "lupin",
                    ImageUrl = ""
                },
                new UserEntry()
                {
                    UserName = "lupan",
                    Password = Helper.CreateMD5("lupan"),
                    SubjectId = "lupan",
                    ImageUrl = ""
                }
            };
        }
    }
}
