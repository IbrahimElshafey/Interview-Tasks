using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTask.Domain
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<List<User>> GetAllUsersAsync();
        Task<bool> UpdateDataAsync(User userToUpdate);
        Task<User> GetUserWithRolesAsync(int userId);
        void UpsertRoles(List<RoleAssociation> userRoleLinks,int userId);
    }
}
