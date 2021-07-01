using Microsoft.EntityFrameworkCore;
using ShopApp.Data.GenericRepository;
using ShopApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Business.Services
{
    public interface IProductService
    {
        Product GetById(int id);
        Product GetByIdWithCategories(int id);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        int GetCountByCategory(string category);
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);
        List<Product> GetAll();
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        bool Update(Product entity, int[] categoryIds);
    }

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public bool Create(Product entity)
        {
            if (Validation(entity))
            {
                _unitofwork.Repository<Product>().Add(entity);
                _unitofwork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _unitofwork.Repository<Product>().Delete(entity);
            _unitofwork.Save();
        }

        public List<Product> GetAll()
        {
            return _unitofwork.Repository<Product>().GetAll().ToList();
        }

        public Product GetById(int id)
        {
            return _unitofwork.Repository<Product>().Get(x => x.Id == id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _unitofwork.Repository<Product>()
                            .GetAll()
                            .Include(x => x.ProductCategories)
                            .ThenInclude(x => x.Category)
                            .FirstOrDefault(i => i.Id == id);
        }

        public int GetCountByCategory(string category)
        {
            var products = _unitofwork.Repository<Product>()
                .GetAll(i => i.IsApproved);

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .Where(i => i.ProductCategories.Any(a => a.Category.Url == category));
            }
            return products.Count();
        }

        public List<Product> GetHomePageProducts()
        {
            return _unitofwork.Repository<Product>()
                           .GetAll(i => i.IsApproved && i.IsHome).ToList();
        }

        public Product GetProductDetails(string url)
        {
            return _unitofwork.Repository<Product>()
                 .GetAll()
                 .Include(i => i.ProductCategories)
                 .ThenInclude(i => i.Category)
                 .FirstOrDefault(i => i.Url == url);
        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            var products = _unitofwork.Repository<Product>()
                 .GetAll(i => i.IsApproved);

            if (!string.IsNullOrEmpty(name))
            {
                products = products
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));
            }
            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetSearchResult(string searchString)
        {
            var products = _unitofwork.Repository<Product>()
                            .GetAll(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))).ToList();

            return products;
        }

        public void Update(Product entity)
        {
            _unitofwork.Repository<Product>().Update(entity);
            _unitofwork.Save();
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            if (Validation(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "Ürün için en az bir kategori seçmelisiniz.";
                    return false;
                }
                var product = _unitofwork.Repository<Product>()
                    .Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.Url = entity.Url;
                    product.IsApproved = entity.IsApproved;
                    product.IsHome = entity.IsHome;

                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        ProductId = entity.Id,
                        CategoryId = catid
                    }).ToList();
                }
                _unitofwork.Save();
                return true;
            }
            return false;
        }

        public string ErrorMessage { get; set; }

        public bool Validation(Product entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "ürün ismi girmelisiniz.\n";
                isValid = false;
            }

            if (entity.Price < 0)
            {
                ErrorMessage += "ürün fiyatı negatif olamaz.\n";
                isValid = false;
            }
            return isValid;
        }
    }
}