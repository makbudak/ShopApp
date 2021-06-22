﻿namespace ShopApp.Model.Entity
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string ImageUrl { get; set; }

        public int Order { get; set; }
    }
}
