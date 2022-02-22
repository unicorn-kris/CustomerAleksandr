using Autofac;
using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using Microsoft.Extensions.CommandLineUtils;
using Serilog;
using System;

namespace CustomerAleksandr.TestgRPCApplication.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ClientModule>();

            var container = builder.Build();

            ILogger logger = container.Resolve<ILogger>();

            CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);

            var url = container.Resolve<CommandLineParameter>();

            try
            {
                CommandOption commandOption = commandLineApplication.Option(
                                                                    "--url <url>",
                                                                    "This url will be "
                                                                    + "use for the connect to the server.",
                                                                    CommandOptionType.SingleValue);

                commandLineApplication.HelpOption("-? | -h | --help");

                commandLineApplication.OnExecute(() =>
                {
                    if (commandOption.HasValue())
                    {
                        url.UrlValue = commandOption.Value();

                        logger.Information($"Value was set for url => Use this url {url.UrlValue}");
                    }
                    else
                    {
                        logger.Information("No value was set for url => Use url which is recorded in config file");
                    }

                    Start(logger, container);

                    return 0;
                });
            }
            catch (Exception ex)
            {
                logger.Information("No value was set for url => Use url which is recorded in config file", ex);
            }

            commandLineApplication.Execute(args);
        }

        public static void Start(ILogger logger, IContainer container)
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

            int choice = int.Parse(Console.ReadLine());
            do
            {
                try
                {
                    container.ResolveNamed<ICommand>(choice.ToString()).Execute();
                }
                catch (Exception ex)
                {
                    logger.Information("One of the reason of problem - incorrect value was set for url. ", ex);
                }
            }
            while (int.TryParse(Console.ReadLine(), out choice));
        }
    }
}
