using Infrastructure.Interfaces;
using Shared.Dtos.User;
using User.Api.Entities;

namespace User.Api.Services.Interfaces
{
    public interface IUserService : IMongoDbRepositoryBase<UserEntry>
    {
        Task Login(UserDto userDto);
        Task<UserDto> Register(CreateUserDto createUserDto);
    }
}
