﻿using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    internal class GetUsersCommand : ICommand
    {
        private UserManagement.UserManagementClient _userClient;
        private ILogger _log;

        public GetUsersCommand(UserManagement.UserManagementClient productClient, ILogger log)
        {
            _userClient = productClient;
            _log = log;
        }

        public async Task Execute()
        {
            try
            {
                var reply = await _userClient.GetUsersAsync(new Empty());

                if (reply != null && reply.UsersList.Any())
                {
                    foreach (var user in reply.UsersList)
                    {
                        Console.WriteLine($"{user.Id}, {user.Name}, {user.Surname}");
                    }
                }

                _log.Information($"GetUsersCommand successfully");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"GetUsersCommand unsuccessfully StatusCode: {ex.StatusCode} Message: {ex.Message}");
            }
        }
    }
}
