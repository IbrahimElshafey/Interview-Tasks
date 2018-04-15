using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using UsersTask.Data;
using UsersTask.Domain;

namespace UsersTask.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly UsersContext _context;
        public IUsersRepository Users { get; }
        public IRolesRepository Roles { get; }

        public UnitOfWork(UsersContext context)
        {
            _context = context;
            Users=new UsersRepository(_context);
            Roles = new RolesRepository(_context);
        }
        public void Dispose()
        {
           _context.Dispose();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }

    public class RolesRepository : Repository<Role>, IRolesRepository
    {
        public RolesRepository(UsersContext context) : base(context)
        {
        }

        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            return await Ctx.UserRoleLinks
                .Include(x => x.Role)
                .Where(r => r.UserId == userId)
                .Select(r => r.Role)
                .ToListAsync();
        }
    }
}
