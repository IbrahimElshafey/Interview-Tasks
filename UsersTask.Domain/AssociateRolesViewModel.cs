using System.Collections.Generic;

namespace UsersTask.Domain
{
    public class AssociateRolesViewModel
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public List<RoleAssociation> RoleAssociations { get; set; } = new List<RoleAssociation>();
    }
}