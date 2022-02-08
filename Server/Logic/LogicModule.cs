using Autofac;
using Repository;

namespace Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterModule(new RepositoryModule());
        }
    }
}
