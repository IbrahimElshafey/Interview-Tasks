using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UsersTask.Domain;

namespace UsersTask.Business
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<User>> GetUsers()
        {
           return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> AddUser(User userToAdd)
        {
            await _unitOfWork.Users.AddAsync(userToAdd);
            _unitOfWork.SaveChanges();
            return userToAdd;
        }

        public bool DeleteUser(User userToDelete)
        {
            var result = _unitOfWork.Users.Remove(userToDelete);
            _unitOfWork.SaveChanges();
            return result;
        }

        public async Task<bool> UpdateUser(User userToUpdate)
        {
            var updateResult = await _unitOfWork.Users.UpdateDataAsync(userToUpdate);
            _unitOfWork.SaveChanges();
            return updateResult;
        }

        public async Task<bool> AssociateRolesToUser(AssociateRolesViewModel rolesVm)
        {
            var user = await _unitOfWork.Users.GetUserWithRolesAsync(rolesVm.UserId);
            _unitOfWork.Users.UpsertRoles(rolesVm.RoleAssociations, rolesVm.UserId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<AssociateRolesViewModel> GetUserRoles(int userId)
        {
           var result=new AssociateRolesViewModel();
           var userRoles = await _unitOfWork.Roles.GetUserRolesAsync(userId);
           var allRoles = await _unitOfWork.Roles.GetAllAsync();
            var user = await _unitOfWork.Users.FindAsync(userId);
            result.UserName=user.UserName;
            foreach (var role in allRoles)
            {
                var roleAssociation=new RoleAssociation{RoleId = role.Id,RoleName = role.Name};
                if (userRoles.Any(r => r.Id == role.Id))
                    roleAssociation.IsActive = true;
                result.RoleAssociations.Add(roleAssociation);
            }
            return result;
        }

        public Task<List<Role>> GetAllRoles()
        {
            return _unitOfWork.Roles.GetAllAsync();
        }
    }
}
