using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsersTask.Domain
{
   public class Role:DomainEntityBase
    {
        public string Name { get; set; }
        public List<UserRoleLink> UserRoleLinks { get; set; }
    }
}
