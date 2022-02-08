using Autofac;

namespace Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryRepository>().As<IRepository>().SingleInstance();
        }
    }
}
