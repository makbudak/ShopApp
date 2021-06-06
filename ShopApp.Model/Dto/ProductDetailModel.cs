using System.Collections.Generic;
using ShopApp.Model.Entity;

namespace ShopApp.Model.Dto
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}