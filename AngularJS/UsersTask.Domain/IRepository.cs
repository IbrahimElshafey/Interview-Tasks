using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UsersTask.Domain
{
    public interface IRepository<TDomainEntityBase> where TDomainEntityBase : class
    {
        TDomainEntityBase Get(int id);
        Task<List<TDomainEntityBase>> GetAllAsync();
        IEnumerable<TDomainEntityBase> Find(Expression<Func<TDomainEntityBase, bool>> predicate);
        Task<TDomainEntityBase> FindAsync(int id);

        Task<bool> AddAsync(TDomainEntityBase entity);
        Task<bool> AddRangeAsync(IEnumerable<TDomainEntityBase> entities);

        bool Remove(TDomainEntityBase entity);
    }
}
