using AutoMapper;
using Infrastructure;
using MongoDB.Driver;
using Shared.Configurations;
using Shared.Dtos.User;
using User.Api.Entities;
using User.Api.Services.Interfaces;

namespace User.Api.Services
{
    public class UserService : MongoDbRepository<UserEntry>, IUserService
    {
        private readonly IMapper _mapper;
        public UserService(
            IMongoClient client,
            DatabaseSettings settings,
            IMapper mapper) : base(client, settings)
        {
            _mapper = mapper;
        }

        public Task Login(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Register(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
