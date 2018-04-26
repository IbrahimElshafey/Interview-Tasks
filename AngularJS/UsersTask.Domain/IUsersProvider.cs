using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTask.Domain
{
    public interface IUsersProvider
    {
        Task<List<User>> GetUsers();
        Task<User> AddUser(User userToAdd);
        bool DeleteUser(User userToDelete);
        Task<bool> UpdateUser(User userToUpdate);

        Task<bool> AssociateRolesToUser(AssociateRolesViewModel rolesVm);
        Task<AssociateRolesViewModel> GetUserRoles(int userId);
    }
}
