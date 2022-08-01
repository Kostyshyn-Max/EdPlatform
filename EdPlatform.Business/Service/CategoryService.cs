using AutoMapper;
using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        public CategoryService()
        {
            _unitOfWork = new();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryModel>());
            var mapper = config.CreateMapper();

            List<CategoryModel> categories = new List<CategoryModel>();
            foreach (var category in await _unitOfWork.CategoryRepository.GetAll())
            {
                categories.Add(mapper.Map<Category, CategoryModel>(category));
            }

            return categories;
        }

        public async Task AddCategory(CategoryModel category)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, Category>());
            var mapper = config.CreateMapper();

            await _unitOfWork.CategoryRepository.Add(mapper.Map<CategoryModel, Category>(category));
            await _unitOfWork.Save();
        }
    }
}
