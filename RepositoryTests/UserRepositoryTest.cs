using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Repository.Entities;
using Repository.SQLite;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryTests
{
    public class UserRepositoryTest
    {
        private List<User> _userList;
        private SQLiteUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userList = new List<User>();

            var mockDbSetUser = new Mock<DbSet<User>>();
            mockDbSetUser
                .As<IQueryable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => _userList.GetEnumerator());
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(() => _userList.AsQueryable().Provider);
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(() => _userList.AsQueryable().Expression);
            mockDbSetUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(() => _userList.AsQueryable().ElementType);


            var mockContext = new Mock<IContext>();
            mockContext
                .Setup(m => m.Users)
                .Returns(mockDbSetUser.Object);
            mockContext
                .Setup(m => m.Add(It.IsAny<User>()))
                .Callback<User>(m => _userList.Add(m));
            mockContext
                .Setup(m => m.Remove(It.IsAny<User>()))
                .Callback<User>(m => _userList.Remove(m));

            _userRepository = new SQLiteUserRepository(mockContext.Object);
        }

        #region add
        [Test]
        public void AddUserTest()
        {
            // Act
            var response = _userRepository.AddUser(
                new User { Name = "Name", Surname = "Surname" });

            // Assert
            Assert.AreEqual(_userList[0].Id, response);
        }

        #endregion

        #region get all
        [Test]
        public void GetAllProductTest()
        {
            _userList.Add(new User { Id = 1, Name = "Name", Surname = "Surname" });
            // Act
            var response = _userRepository.GetAll();

            // Assert
            Assert.AreEqual(_userList[0].Id, response[0].Id);
        }

        #endregion

        #region Get by id
        [Test]
        public void GetByIdProductTest()
        {
            _userList.Add(new User { Id = 1, Name = "Name", Surname = "Surname" });

            // Act
            var response = _userRepository.GetUserById(1);

            // Assert
            Assert.AreEqual(1, response.Id);
        }

        #endregion
    }
}
