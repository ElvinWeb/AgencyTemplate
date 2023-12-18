using Agency.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.DataAccessLayer
{
    public class AgencyDbContext : IdentityDbContext
    {
        public AgencyDbContext(DbContextOptions<AgencyDbContext> options) : base(options) { }
        public DbSet<Setting> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}
