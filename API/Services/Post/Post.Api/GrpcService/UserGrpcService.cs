using User.Grpc.Protos;

namespace Post.Api.GrpcService
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _service;

        public UserGrpcService(UserProtoService.UserProtoServiceClient service)
        {
            _service = service;
        }

        public async Task<UserReply> GetUserByUsername(string username)
          => await _service.GetUserByUsernameAsync(new LoginRequest { UserName = username });
        
    }
}
