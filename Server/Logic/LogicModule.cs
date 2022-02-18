using Autofac;
using Logic.Interfaces;
using Logic.Services;
using Repository;

namespace Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterModule(new RepositoryModule());
        }
    }
}
