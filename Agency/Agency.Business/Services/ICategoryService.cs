using Agency.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(Category entity);
        Task Delete(int id);
        IQueryable<Category> GetCategoryTable();
        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task UpdateAsync(Category entity);
    }
}
