using System;

namespace ShopApp.Model.Dto
{
    public class TodoCategoryModel : BaseModel
    {
        public string Name { get; set; }
    }

    public class TodoStatusModel : BaseModel
    {
        public int TodoCategoryId { get; set; }

        public string TodoCategoryName { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class TodoModel : BaseModel
    {
        public int TodoCategoryId { get; set; }

        public string CategoryName { get; set; }

        public int TodoStatusId { get; set; }

        public string StatusName { get; set; }

        public int UserId { get; set; }

        public string NameSurname { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
