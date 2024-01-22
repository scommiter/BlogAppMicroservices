using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using User.Grpc.Helpers;
using User.Grpc.Protos;
using User.Grpc.Repositories.Interfaces;

namespace User.Grpc.Services
{
    public class UserService : UserProtoService.UserProtoServiceBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        const int unitValue = 1000;
        private readonly IConfiguration _config;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IConfiguration config)
        {
            _logger = logger;
            _userRepository = userRepository;
            _config = config;
        }

        public override async Task<Empty> Register(UserRequest request, ServerCallContext context)
        {
            var mainUser = await _userRepository.GetUserByUsername(request.UserName);
            if (mainUser == null)
            {
                var passwordHash = Helper.CreateMD5(request.Password);
                var entity = new Entities.UserEntry
                {
                    UserName = request.UserName,
                    Password = passwordHash,
                    SubjectId = request.UserName,
                    ImageUrl = "/images/user.png",
                };
                await _userRepository.CreateAsync(entity);
                _logger.LogInformation($"Insert success {request.UserName}");
            }
            else
            {
                _logger.LogInformation($"{request.UserName} has already exist!");
            }

            return new Empty();
        }

        public override async Task<UserReply> Login(Protos.LoginRequest request, ServerCallContext context)
        {
            var mainUser = await _userRepository.GetUserByUsername(request.UserName);
            if (mainUser != null)
            {
                var md5Hash = Helper.CreateMD5(request.PassWord);
                if (mainUser.Password == md5Hash)
                {
                    _logger.LogInformation($"{request.UserName} login success");
                    return new UserReply
                    {
                        UserName = mainUser.UserName,
                        SubjectId = mainUser.SubjectId,
                        ImageUrl = $"{_config["BaseUrl:UserApiUrl"]}{mainUser.ImageUrl}"
                    };
                }
            }
            return new UserReply { UserName = mainUser.UserName, SubjectId = mainUser.SubjectId, ImageUrl = mainUser.ImageUrl };
        }

        public override async Task<UserReply> GetUserByUsername(Protos.LoginRequest request, ServerCallContext context)
        {
            var mainUser = await _userRepository.GetUserByUsername(request.UserName);
            if (mainUser != null)
            {
                _logger.LogInformation($"{request.UserName} is get success");
                return new UserReply
                {
                    UserName = mainUser.UserName,
                    SubjectId = mainUser.SubjectId,
                    ImageUrl = $"{mainUser.ImageUrl}",
                };
            }
            return new UserReply { UserName = "", SubjectId = "", ImageUrl = "" };
        }
    }
}
