using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Testing;
using Grpc.Core.Utils;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceTests
{
    public class UserServiceTest
    {
        private Mock<IUserService> _mockUserService;
        private UserService _userService;
        private ServerCallContext _testServerCallContext;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockUserService.Setup(m => m.AddUser(It.IsAny<Logic.Entities.User>())).Returns(1);
            _mockUserService.Setup(m => m.GetAll()).Returns(new List<Logic.Entities.User> { new Logic.Entities.User { Name = "Name", Surname = "Surname", Id = 1 } });
            _mockUserService.Setup(m => m.GetUserById(1)).Returns(new Logic.Entities.User { Name = "Name", Surname = "Surname", Id = 1 });

            _testServerCallContext = TestServerCallContext.Create("fooMethod", null, DateTime.UtcNow.AddHours(1), new Metadata(), CancellationToken.None, "127.0.0.1", null, null, (metadata) => TaskUtils.CompletedTask, () => new WriteOptions(), (writeOptions) => { });

            var loggerUserService = LoggerFactory.Create(b => b.AddConsole()).CreateLogger<UserService>();

            _userService = new UserService(loggerUserService, _mockUserService.Object);
        }

        #region add
        [Test]
        public async Task AddUserTest()
        {
            // Act
            var response = await _userService.AddUser(
                new User { Name = "Name", Surname = "Surname" }, _testServerCallContext);

            // Assert
            Assert.AreEqual(1, response.Id);
        }

        #endregion

        #region get all
        [Test]
        public async Task GetAllUserTest()
        {
            // Act
            var response = await _userService.GetUsers(new Empty(), _testServerCallContext);

            // Assert
            Assert.AreEqual(1, response.UsersList.Count);
            Assert.AreEqual(1, response.UsersList[0].Id);
        }

        #endregion

        #region Get by id
        [Test]
        public async Task GetByIdUserTest()
        {
            // Act
            var response = await _userService.GetUserById(new UserId { Id = 1 }, _testServerCallContext);

            // Assert
            _mockUserService.Verify(p => p.GetUserById(1));
            Assert.AreEqual(1, response.Id);
        }

        #endregion
    }
}
