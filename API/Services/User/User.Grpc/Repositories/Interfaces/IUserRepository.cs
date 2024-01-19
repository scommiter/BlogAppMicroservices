using Infrastructure.Interfaces;
using User.Grpc.Entities;

namespace User.Grpc.Repositories.Interfaces
{
    public interface IUserRepository : IMongoDbRepositoryBase<UserEntry>
    {
        Task Register(UserEntry userEntry);
        Task<UserEntry> GetUserByUsername(string userName);
    }
}
