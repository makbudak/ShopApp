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
        List<Product> Get();
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        bool Update(Product entity, int[] categoryIds);
    }

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Product entity)
        {
            if (Validation(entity))
            {
                _unitOfWork.Repository<Product>().Add(entity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _unitOfWork.Repository<Product>().Delete(entity);
            _unitOfWork.Save();
        }

        public List<Product> Get()
        {
            return _unitOfWork.Repository<Product>().Where().ToList();
        }

        public Product GetById(int id)
        {
            return _unitOfWork.Repository<Product>().FirstOrDefault(x => x.Id == id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _unitOfWork.Repository<Product>()
                            .Where()
                            .Include(x => x.ProductCategoryItems)
                            .FirstOrDefault(i => i.Id == id);
        }

        public int GetCountByCategory(string category)
        {
            var products = _unitOfWork.Repository<Product>()
                .Where(i => i.IsApproved)
                .Include(x => x.ProductCategoryItems)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products
                    .Include(i => i.ProductCategoryItems)
                    .Where(i => i.ProductCategoryItems.Any(a => a.ProductCategory.Name == category));
            }
            return products.Count();
        }

        public List<Product> GetHomePageProducts()
        {
            return _unitOfWork.Repository<Product>()
                           .Where(i => i.IsApproved && i.IsHome).ToList();
        }

        public Product GetProductDetails(string url)
        {
            return _unitOfWork.Repository<Product>()
                 .Where()
                 .Include(i => i.ProductCategoryItems)
                 .FirstOrDefault(i => i.Url == url);
        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            var products = _unitOfWork.Repository<Product>()
                 .Where(i => i.IsApproved);

            if (!string.IsNullOrEmpty(name))
            {
                products = products
                    .Include(i => i.ProductCategoryItems)
                    .ThenInclude(x => x.ProductCategory)
                    .Where(i => i.ProductCategoryItems.Any(a => a.ProductCategory.Name == name));
            }
            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetSearchResult(string searchString)
        {
            var products = _unitOfWork.Repository<Product>()
                            .Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))).ToList();

            return products;
        }

        public void Update(Product entity)
        {
            _unitOfWork.Repository<Product>().Update(entity);
            _unitOfWork.Save();
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
                var product = _unitOfWork.Repository<Product>()
                    .Include(i => i.ProductCategoryItems)
                    .FirstOrDefault(i => i.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.Url = entity.Url;
                    product.IsApproved = entity.IsApproved;
                    product.IsHome = entity.IsHome;

                    product.ProductCategoryItems = categoryIds
                        .Select(catid => new ProductCategoryItem()
                        {
                            ProductId = entity.Id,
                            ProductCategoryId = catid
                        }).ToList();
                }
                _unitOfWork.Save();
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