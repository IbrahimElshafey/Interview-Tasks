using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersTask.Data;
using UsersTask.Domain;

namespace UsersTask.Repositories
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity:DomainEntityBase
    {
        protected UsersContext Ctx;

        public Repository(UsersContext context)
        {
            Ctx = context;
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await Ctx.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await Ctx.Set<TEntity>().AddAsync(entity);
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Ctx.Set<TEntity>().AddRangeAsync(entities);
            return true;
        }

        public bool Remove(TEntity entity)
        {
            Ctx.Set<TEntity>().Remove(entity);
            return true;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Ctx.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Ctx.Set<TEntity>().Where(predicate).AsEnumerable();
        }

        public TEntity Get(int id)
        {
            return Ctx.Set<TEntity>().Find(id);
        }
    }
}
