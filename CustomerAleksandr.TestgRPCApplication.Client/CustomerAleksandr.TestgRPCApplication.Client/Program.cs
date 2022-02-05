using Grpc.Net.Client;
using System;
using CustomerAleksandr.TestgRPCApplication;

namespace CustomerAleksandr.TestgRPCApplication.Client
{
    class Program
    {
        static int Read()
        {
            int result = -1;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Enter a valid value");
            }
            return result;
        }
        static void Main(string[] args)
        {
            //the way for the server
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            //create client
            var client = new UserManagement.UserManagementClient(channel);

            Console.WriteLine("Select an action\n" +
                "0 - Add user\n" +
                "1 - Get all users\n" +
                "2 - Get user by id\n" +
                "3 - Exit");

            int choice = Read();

            while (choice != 3)
            {
                switch (choice)
                {
                    case 0:
                        AddUser(client);
                        break;
                    case 1:
                        GetUsers(client);
                        break;
                    case 2:
                        GetUserById(client);
                        break;
                    default:
                        Console.WriteLine("Enter a valid value");
                        break;
                }
                choice = Read();
            }
        }

        private static async void GetUserById(UserManagement.UserManagementClient client)
        {
            Console.WriteLine("Enter id");

            int userId = Read();

            var reply = await client.GetUserByIdAsync(new UserId { Id = userId });

            if (reply != null)
            {
                Console.WriteLine($"{reply.Id}, {reply.Name}, {reply.Surname}");
            }
            else
            {
                Console.WriteLine("Enter a valid value");
            }
        }

        private static async void GetUsers(UserManagement.UserManagementClient client)
        {
            var reply = await client.GetUsersAsync(new GetUsersParameter { });

            foreach(var user in reply.UsersList)
            {
                Console.WriteLine($"{user.Id}, {user.Name}, {user.Surname}");
            }
        }

        private static async void AddUser(UserManagement.UserManagementClient client)
        {
            var newUser = new User();

            Console.WriteLine("Enter name: ");
            newUser.Name = Console.ReadLine();

            Console.WriteLine("Enter surname: ");
            newUser.Surname = Console.ReadLine();

            var reply = await client.AddUserAsync(newUser);

            Console.WriteLine($"Code: {reply.TypeOfResponse}, {reply.ResponseMessage}");
        }

        
    }
}
