﻿using System.Security.Claims;

namespace Chat.Api.Services
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
            return _context.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }
    }
}
