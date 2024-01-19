using Shared.Dtos.User;
using User.Grpc.Protos;

namespace User.Api.GrpcService
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _service;

        public UserGrpcService(UserProtoService.UserProtoServiceClient service)
        {
            _service = service;
        }

        public async Task Register(CreateUserDto userDto)
        {
            var request = new UserRequest
            {
                UserName = userDto.UserName,
                Password = userDto.Password
            };

            await _service.RegisterAsync(request);
        }

        public async Task<UserReply> Login(UserLoginDto login)
        {
            var request = new LoginRequest
            {
                UserName = login.UserName,
                PassWord = login.Password
            };

            return await _service.LoginAsync(request);
        }
    }
}
