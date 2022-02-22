using Autofac;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.ReaderServices;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands;
using CustomerAleksandr.TestgRPCApplication.Client.Decorators;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CustomerAleksandr.TestgRPCApplication.Client
{
    internal class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .Build()).As<IConfiguration>().SingleInstance();

            builder.Register(c => GrpcChannel.ForAddress(c.Resolve<IConfiguration>().GetSection("URL").Value)).As<ChannelBase>().SingleInstance();

            builder.RegisterType<ConsoleReaderService>().As<IReaderService>();

            builder.RegisterType<UserManagement.UserManagementClient>();

            builder.RegisterType<ProductManagement.ProductManagementClient>();

            builder.RegisterType<AddUserCommand>().Named<ICommand>("0");

            builder.RegisterType<GetUsersCommand>().Named<ICommand>("1");

            builder.RegisterType<GetUserByIdCommand>().Named<ICommand>("2");

            builder.RegisterType<GetUsersProductsCommand>().Named<ICommand>("3");

            builder.RegisterType<AddProductCommand>().Named<ICommand>("4");

            builder.RegisterType<GetProductsCommand>().Named<ICommand>("5");

            builder.RegisterType<GetProductByIdCommand>().Named<ICommand>("6");

            builder.RegisterType<GetProductsUsersCommand>().Named<ICommand>("7");

            builder.RegisterType<DeleteProductCommad>().Named<ICommand>("8");

            builder.RegisterType<BuyProductCommand>().Named<ICommand>("9");

            builder.Register<ILogger>(log =>
            {
                return new LoggerConfiguration()
                  .WriteTo.Console()
                  .WriteTo.File("Log.txt")
                  .CreateLogger();
            }).SingleInstance();

            builder.RegisterDecorator<LoggingDecorator, ICommand>();

            builder.RegisterDecorator<TimingDecorator, ICommand>();
        }
    }
}