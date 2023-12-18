using Agency.Core.Models;
using Agency.Data.DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agency.MVC.ViewService
{
    public class LayoutService
    {
        private readonly AgencyDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutService(AgencyDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<Setting>> GetSetting()
        {
            List<Setting> settings = await _context.Services.ToListAsync();

            return settings;

        }
        public async Task<User> GetUser()
        {

            User user = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }

            return user;
        }
    }
}
