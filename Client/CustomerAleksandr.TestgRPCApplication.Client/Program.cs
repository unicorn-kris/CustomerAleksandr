using Autofac;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using System;

namespace CustomerAleksandr.TestgRPCApplication.Client
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Select an action\n" +
                              "0 - Add user\n" +
                              "1 - Get all users\n" +
                              "2 - Get user by id\n" +
                              "3 - Get user's products\n" +
                              "4 - Add product\n" +
                              "5 - Get all products\n" +
                              "6 - Get product by id\n" +
                              "7 - Get product's users\n" +
                              "8 - Delete product\n" +
                              "9 - Buy product");

            var builder = new ContainerBuilder();
            builder.RegisterModule<ClientModule>();

            var container = builder.Build();

            int choice = int.Parse(Console.ReadLine());
            do
            {
                container.ResolveNamed<ICommand>(choice.ToString()).Execute();
            }
            while (int.TryParse(Console.ReadLine(), out choice));
        }
    }
}
