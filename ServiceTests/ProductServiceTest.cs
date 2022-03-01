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
    public class ProductServiceTest
    {
        private Mock<IProductService> _mockProductService;
        private ProductService _productService;
        private ServerCallContext _testServerCallContext;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _mockProductService.Setup(m => m.AddProduct(It.IsAny<Logic.Entities.Product>())).Returns(1);
            _mockProductService.Setup(m => m.GetAll()).Returns(new List<Logic.Entities.Product> { new Logic.Entities.Product { Title = "Title", Count = 10, Price = 12, Id = 1 } });
            _mockProductService.Setup(m => m.DeleteProduct(1));
            _mockProductService.Setup(m => m.BuyProduct(It.IsAny<int>(), It.IsAny<int>()));
            _mockProductService.Setup(m => m.GetProductById(1)).Returns(new Logic.Entities.Product { Title = "Title", Count = 10, Price = 12, Id = 1 });

            _testServerCallContext = TestServerCallContext.Create("testMethod", null, DateTime.UtcNow.AddHours(1), new Metadata(), CancellationToken.None, "127.0.0.1", null, null, (metadata) => TaskUtils.CompletedTask, () => new WriteOptions(), (writeOptions) => { });

            var loggerProductService = LoggerFactory.Create(b => b.AddConsole()).CreateLogger<ProductService>();

            _productService = new ProductService(loggerProductService, _mockProductService.Object);
        }

        #region add
        [Test]
        public async Task AddProductTest()
        {
            // Act
            var response = await _productService.AddProduct(
                new Product { Title = "Title", Count = 10, Price = 12 }, _testServerCallContext);

            // Assert
            Assert.AreEqual(1, response.Id);
        }

        #endregion

        #region get all
        [Test]
        public async Task GetAllProductTest()
        {
            // Act
            var response = await _productService.GetProducts(new Empty(), _testServerCallContext);

            // Assert
            Assert.AreEqual(1, response.ProductsList.Count);
            Assert.AreEqual(1, response.ProductsList[0].Id);
        }

        #endregion

        #region Delete
        [Test]
        public async Task DeleteProductTest()
        {
            // Act
            var response = await _productService.DeleteProduct(new ProductId { Id = 1 }, _testServerCallContext);

            // Assert
            _mockProductService.Verify(p => p.DeleteProduct(1));
        }

        #endregion

        #region Buy
        [Test]
        public async Task BuyProductTest()
        {
            // Act
            var response = await _productService.BuyProduct(new BuyProductRequest { UserId = 1, ProductId = 1 }, _testServerCallContext);

            // Assert
            _mockProductService.Verify(p => p.BuyProduct(1, 1));
        }

        #endregion

        #region Get by id
        [Test]
        public async Task GetByIdProductTest()
        {
            // Act
            var response = await _productService.GetProductById(new ProductId { Id = 1 }, _testServerCallContext);

            // Assert
            _mockProductService.Verify(p => p.GetProductById(1));
            Assert.AreEqual(1, response.Id);
        }

        #endregion
    }
}