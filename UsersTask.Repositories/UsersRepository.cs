using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersTask.Data;
using UsersTask.Domain;

namespace UsersTask.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(UsersContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await Ctx.Users.ToListAsync();
        }

        public async Task<bool> UpdateDataAsync(User userToUpdate)
        {
            var entityInDb =await Ctx.Users.FindAsync(userToUpdate.Id);
            if (entityInDb == null) return false;
            Ctx.Entry(entityInDb).CurrentValues.SetValues(userToUpdate);
            return true;
        }

        public async Task<User> GetUserWithRolesAsync(int userId)
        {
            return await Ctx.Users
                .Include(x=>x.UserRoleLinks)
                .ThenInclude(x=>x.Role)
                .FirstAsync(x=>x.Id==userId);
        }

        public void UpsertRoles(List<RoleAssociation> userRoleLinks, int userId)
        {
            foreach (var entity in userRoleLinks)
            {
                var entityInDb = Ctx.UserRoleLinks.Find(userId,entity.RoleId);
                if (entityInDb != null)
                {
                    if (entity.IsActive)
                        Ctx.Entry(entityInDb).CurrentValues.SetValues(entity);
                    else
                        Ctx.UserRoleLinks.Remove(entityInDb);
                }
                else
                {
                    if (entity.IsActive)
                        Ctx.Set<UserRoleLink>().Add(new UserRoleLink
                        {
                            RoleId = entity.RoleId,
                            UserId = userId
                        });
                }
            }
        }
    }
}
