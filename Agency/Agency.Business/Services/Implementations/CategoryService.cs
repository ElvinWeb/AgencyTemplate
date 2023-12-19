using Agency.Business.CustomExceptions;
using Agency.Core.Models;
using Agency.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task CreateAsync(Category category)
        {
            if (_categoryRepository.Table.Any(x => x.Name.ToLower() == category.Name.ToLower())) 
            {
                throw new InvalidAlreadyCreated("Name", "category has already created!");
            }

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            Category entity = await _categoryRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            _categoryRepository.Delete(entity);
            await _categoryRepository.CommitAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync(x => x.IsDeleted == false, "Portfolios");
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category entity = await _categoryRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (entity is null) throw new NullReferenceException();
            return entity;
        }

        public IQueryable<Category> GetCategoryTable()
        {
            var query = _categoryRepository.Table.AsQueryable();
            return query;
        }

        public async Task UpdateAsync(Category category)
        {
            Category existEntity = await _categoryRepository.GetByIdAsync(x => x.Id == category.Id && x.IsDeleted == false);

            if (_categoryRepository.Table.Any(x => x.Name.ToLower() == category.Name.ToLower() && existEntity.Id != category.Id))
            {
                throw new InvalidAlreadyCreated("Name", "category has already created!");
            }

            existEntity.Name = category.Name;
          

            await _categoryRepository.CommitAsync();
        }
    }
}
