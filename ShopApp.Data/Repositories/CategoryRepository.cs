using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Model.Entity;

namespace ShopApp.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByIdWithProducts(int categoryId);

        void DeleteFromCategory(int productId, int categoryId);
    }

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ShopContext context): base(context)
        {
            
        }
          
        private ShopContext ShopContext
        {
            get {return context as ShopContext; }
        }
        public void DeleteFromCategory(int productId, int categoryId)
        {
                var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                ShopContext.Database.ExecuteSqlRaw(cmd,productId,categoryId);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
                return ShopContext.Categories
                            .Where(i=>i.CategoryId==categoryId)
                            .Include(i=>i.ProductCategories)
                            .ThenInclude(i=>i.Product)
                            .FirstOrDefault();
        }


    }
}