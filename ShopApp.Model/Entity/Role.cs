using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Model.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }
    }
}
