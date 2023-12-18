using Agency.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services
{
    public interface IPortfolioService
    {
        Task CreateAsync(Portfolio entity);
        Task Delete(int id);
        Task SoftDelete(int id);
        IQueryable<Portfolio> GetPortfolioTable();
        Task<Portfolio> GetByIdAsync(int id);
        Task<List<Portfolio>> GetAllAsync();
        Task UpdateAsync(Portfolio entity);
    }
}
