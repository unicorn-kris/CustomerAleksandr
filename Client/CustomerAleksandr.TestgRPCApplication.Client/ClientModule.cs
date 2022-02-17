using Autofac;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.ReaderCommands;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using Grpc.Net.Client;

namespace CustomerAleksandr.TestgRPCApplication.Client
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => GrpcChannel.ForAddress("https://localhost:5001")).As<ChannelBase>().SingleInstance();

            builder.RegisterType<ConsoleReaderCommand>().As<IReaderCommand>();

            builder.RegisterType<UserManagement.UserManagementClient>();

            builder.RegisterType<ProductManagement.ProductManagementClient>();

            builder.RegisterType<AddUserCommand>().As<ICommand>().Named<ICommand>("0");

            builder.RegisterType<GetUsersCommand>().As<ICommand>().Named<ICommand>("1");

            builder.RegisterType<GetUserByIdCommand>().As<ICommand>().Named<ICommand>("2");

            builder.RegisterType<GetUsersProductsCommand>().As<ICommand>().Named<ICommand>("3");

            builder.RegisterType<AddProductCommand>().Named<ICommand>("4");

            builder.RegisterType<GetProductsCommand>().As<ICommand>().Named<ICommand>("5");

            builder.RegisterType<GetProductByIdCommand>().As<ICommand>().Named<ICommand>("6");

            builder.RegisterType<GetProductsUsersCommand>().As<ICommand>().Named<ICommand>("7");

            builder.RegisterType<DeleteProductCommad>().As<ICommand>().Named<ICommand>("8");

            builder.RegisterType<BuyProductCommand>().As<ICommand>().Named<ICommand>("9");

        }
    }
}
