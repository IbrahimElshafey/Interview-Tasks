using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using UsersTask.Business;
using UsersTask.Data;
using UsersTask.Domain;
using UsersTask.Repositories;

namespace UsersTask.WebClient
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersContext>().As<UsersContext>().InstancePerLifetimeScope();

            builder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RolesRepository>().As<IRolesRepository>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<SeedData>().As<ISeedData>().InstancePerLifetimeScope();

            builder.RegisterType<UsersProvider>().As<IUsersProvider>().InstancePerLifetimeScope();
        }
    }
}
