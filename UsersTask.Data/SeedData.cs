using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using UsersTask.Domain;

namespace UsersTask.Data
{
    public class SeedData : ISeedData
    {
        private readonly UsersContext _ctx;

        public SeedData(UsersContext ctx)
        {
            _ctx = ctx;

            
        }

        public void Seed()
        {
           
            try
            {
                using (_ctx)
                {
                    if (AllMigrationsApplied()) return;
                    _ctx.Database.Migrate();
                    Upsert(new List<Role>(new[]
                    {
                        new Role{Id = 1,Name = "Admin"},
                        new Role{Id = 2,Name = "Premium User"},
                        new Role{Id = 3,Name = "User"}
                    }));
                    Upsert(new List<User>(new[]
                    {
                        new User{UserName = "Mohamed Hassan",Email = "User1@mail.com",Password = "Password123"}, 
                        new User{UserName = "Ahmed Ibrahim",Email = "User2@mail.com",Password = "Password123"}, 
                        new User{UserName = "Ali Taha",Email = "User3@mail.com",Password = "Password123"}, 
                        new User{UserName = "Mansoor Gad",Email = "User4@mail.com",Password = "Password123"}, 
                    }));
                    _ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private void Upsert<TEntity>(List<TEntity> entities) where TEntity : DomainEntityBase
        {
            foreach (var entity in entities)
            {
                var entityInDb = _ctx.Set<TEntity>().Find(entity.Id);
                if (entityInDb != null)
                {
                    _ctx.Entry(entityInDb).CurrentValues.SetValues(entity);
                }
                else
                {
                    _ctx.Set<TEntity>().Add(entity);
                }
            }
        }

        public bool AllMigrationsApplied()
        {
            var applied = _ctx.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = _ctx.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
