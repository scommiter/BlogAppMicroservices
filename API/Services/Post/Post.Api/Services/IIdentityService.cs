using System.Security.Claims;

namespace Post.Api.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();
    }

    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserIdentity()
        {
            foreach (var claim in _context.HttpContext.User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            var test2 = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
