using Autofac;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.SQLite;

namespace Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteUserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<SQLiteProductRepository>().As<IProductRepository>().SingleInstance();

            builder.RegisterType<IContext>().As<DbContext>().SingleInstance();

            builder.RegisterType<ShopContext>().As<IContext>().SingleInstance();
        }
    }
}
