using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface ICategoryService
    {
        Category GetById(int id);

        Category GetByIdWithProducts(int categoryId);

        List<Category> GetAll();

        void Create(Category entity);

        void Update(Category entity);

        void Delete(Category entity);

        void DeleteFromCategory(int productId, int categoryId);
    }


    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public void Create(Category entity)
        {
            _unitofwork.Repository<Category>().Add(entity);
            _unitofwork.Save();
        }

        public void Delete(Category entity)
        {
            _unitofwork.Repository<Category>().Delete(entity);
            _unitofwork.Save();
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            var productCategory = _unitofwork.Repository<ProductCategory>()
                .Get(x => x.CategoryId == categoryId && x.ProductId == productId);
            if (productCategory != null)
            {
                _unitofwork.Repository<ProductCategory>().Delete(productCategory);
                _unitofwork.Save();
            }
        }

        public List<Category> GetAll()
        {
            return _unitofwork.Repository<Category>().GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _unitofwork.Repository<Category>().Get(x => x.Id == id);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            var category = _unitofwork.Repository<Category>()
                .GetAll(i => i.Id == categoryId)
                .Include(i => i.ProductCategories)
                .ThenInclude(i => i.Product)
                .FirstOrDefault();
            return category;
        }

        public void Update(Category entity)
        {
            _unitofwork.Repository<Category>().Update(entity);
            _unitofwork.Save();
        }
    }
}