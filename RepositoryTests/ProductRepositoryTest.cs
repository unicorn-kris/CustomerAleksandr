using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Repository.Entities;
using Repository.SQLite;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryTests
{
    public class ProductRepositoryTest
    {
        private List<Product> _productList;
        private List<User> _userList;
        private SQLiteProductRepository _productRepository;

        [SetUp]
        public void Setup()
        {
            _productList = new List<Product>();
            _userList = new List<User>(){ new User {Id = 1, Name = "Name", Surname = "Surname" } };

            var mockDbSetProduct = new Mock<DbSet<Product>>();
            mockDbSetProduct
                .As<IQueryable<Product>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => _productList.GetEnumerator());
            mockDbSetProduct.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(() => _productList.AsQueryable().Provider);
            mockDbSetProduct.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(() =>  _productList.AsQueryable().Expression);
            mockDbSetProduct.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(() =>  _productList.AsQueryable().ElementType);

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
                .Setup(m => m.Products)
                .Returns(mockDbSetProduct.Object);
            mockContext
                .Setup(m => m.Add(It.IsAny<Product>()))
                .Callback<Product>(m => _productList.Add(m));
            mockContext
                .Setup(m => m.Remove(It.IsAny<Product>()))
                .Callback<Product>(m => _productList.Remove(m));
            mockContext
                .Setup(m => m.Users)
                .Returns(mockDbSetUser.Object);

            _productRepository = new SQLiteProductRepository(mockContext.Object);
        }

        [Test]
        public void AddProductTest()
        {
            // Act
            var response = _productRepository.AddProduct(
                new Product { Title = "Title", Count = 10, Price = 12 });

            // Assert
            Assert.AreEqual(_productList[0].Id, response);
        }

        [Test]
        public void GetAllProductTest()
        {
            _productList.Add(new Product { Title = "Title", Count = 10, Price = 12, Id = 1 });
            // Act
            var response = _productRepository.GetAll();

            // Assert
            Assert.AreEqual(_productList[0].Id, response[0].Id);
        }

        [Test]
        public void DeleteProductTest()
        {
            _productList.Add( new Product { Title = "Title", Count = 10, Price = 12, Id = 1 } );

            // Act
            _productRepository.DeleteProduct(1);

            // Assert
            Assert.AreEqual(_productList.Count, 0);
        }

        [Test]
        public void BuyProductTest()
        {
            _productList.Add( new Product { Title = "Title", Count = 10, Price = 12, Id = 1 } );

            // Act
            _productRepository.BuyProduct(1, 1);

            // Assert
            Assert.AreEqual(_userList[0].Products[0].Id, _productList[0].Id);
        }

        [Test]
        public void GetByIdProductTest()
        {
            _productList.Add( new Product { Title = "Title", Count = 10, Price = 12, Id = 1 } );

            // Act
            var response = _productRepository.GetProductById(1);

            // Assert
            Assert.AreEqual(1, response.Id);
        }
    }
}
