namespace Shared.Dtos.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public string SubjectId { get; set; }

        public string ImageUrl { get; set; }
    }
}
