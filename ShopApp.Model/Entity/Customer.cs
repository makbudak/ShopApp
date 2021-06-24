using System;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Model.Entity
{
    public class Customer : BaseEntity
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Surname { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Password { get; set; }

        public bool EmailConfirmed { get; set; }

        [StringLength(500)]
        public string PasswordHashCode { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
