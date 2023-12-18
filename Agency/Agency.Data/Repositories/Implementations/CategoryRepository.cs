using Agency.Core.Models;
using Agency.Core.Repositories;
using Agency.Data.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AgencyDbContext context) : base(context)
        {

        }
    }
}
