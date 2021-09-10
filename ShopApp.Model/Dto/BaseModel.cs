
using System.Collections.Generic;

namespace ShopApp.Model.Dto
{
    public class BaseModel
    {
        public int Id { get; set; }
    }

    public class Pagination<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
    }

    public class BaseNullableModel
    {
        public int? Id { get; set; }
    }

    public class PageNumberModel
    {
        public PageNumberModel()
        {
            PageSize = 5;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
