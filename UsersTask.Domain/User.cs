using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsersTask.Domain
{
    public class User:DomainEntityBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserRoleLink> UserRoleLinks { get; set; }
    }
}
