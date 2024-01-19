using Infrastructure;
using MongoDB.Driver;
using Shared.Configurations;
using User.Grpc.Entities;
using User.Grpc.Repositories.Interfaces;

namespace User.Grpc.Repositories
{
    public class UserRepository : MongoDbRepository<UserEntry>, IUserRepository
    {
        public UserRepository(IMongoClient client, DatabaseSettings settings) : base(client, settings)
        {
        }

        public async Task<UserEntry> GetUserByUsername(string userName)
            => Collection.AsQueryable().Where(x => x.UserName == userName)?.FirstOrDefault()!;

        public async Task Register(UserEntry userEntry)
            => await CreateAsync(userEntry);
    }
}
