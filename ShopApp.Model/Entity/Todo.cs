using System;

namespace ShopApp.Model.Entity
{
    public class Todo : BaseEntity
    {
        public int TodoStatusId { get; set; }

        public TodoStatus TodoStatus { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }
    }
}
