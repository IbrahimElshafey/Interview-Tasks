using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsersTask.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IRolesRepository Roles { get; }
        bool SaveChanges();
    }
}
