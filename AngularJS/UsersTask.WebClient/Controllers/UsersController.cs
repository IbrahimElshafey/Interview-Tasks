using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersTask.Domain;

namespace UsersTask.WebClient.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly IUsersProvider _usersProvider;

        public UsersController(IUsersProvider usersProvider)
        {
            _usersProvider = usersProvider;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _usersProvider.GetUsers();
        }

        [HttpPost]
        public async Task<User> AddUser([FromBody]User userToAdd)
        {
            return await _usersProvider.AddUser(userToAdd);
        }

        [HttpPost]
        public bool DeleteUser([FromBody]User userToDelete)
        {
            return  _usersProvider.DeleteUser(userToDelete);
        }

        [HttpPost]
        public async Task<bool> UpdateUser([FromBody]User userToUpdate)
        {
            return await _usersProvider.UpdateUser(userToUpdate);
        }

        [HttpPost]
        public async Task<bool> AssociateRolesToUserAsync([FromBody]AssociateRolesViewModel rolesVm)
        {
            return await _usersProvider.AssociateRolesToUser(rolesVm);
        }

        [HttpGet]
        public async Task<AssociateRolesViewModel> GetUserRoles(int userId)
        {
            return await _usersProvider.GetUserRoles(userId);
        }
    }
}
