using System.Collections.Generic;
using ShopApp.Model.Entity;

namespace ShopApp.Model.Dto.Product
{
    public class ProductDetailModel
    {
        public Entity.Product Product { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}