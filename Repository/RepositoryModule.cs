using Autofac;
using Repository.Interfaces;
using Repository.SQLite;

namespace Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqliteUserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<SQLiteProductRepository>().As<IProductRepository>().SingleInstance();
        }
    }
}
