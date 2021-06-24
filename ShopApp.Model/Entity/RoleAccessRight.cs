using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Model.Entity
{
    public class RoleAccessRight : BaseEntity
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int AccessRightId { get; set; }

        public AccessRight AccessRight { get; set; }
    }
}
