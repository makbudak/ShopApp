using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Dto
{
    public class ProductCategoryModel : BaseModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Kategori için 3-100 arasında değer giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Url için 3-100 arasında değer giriniz.")]

        public string Url { get; set; }

        public int? ParentId { get; set; }
    }

    public class TreeProductCategoryModel : BaseNullableModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Url { get; set; }
        public List<TreeProductCategoryModel> Items { get; set; }
    }
}