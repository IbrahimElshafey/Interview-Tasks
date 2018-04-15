using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersTask.Domain
{
    public interface IRolesRepository : IRepository<Role>
    {
        Task<List<Role>> GetUserRolesAsync(int userId);
    }
}