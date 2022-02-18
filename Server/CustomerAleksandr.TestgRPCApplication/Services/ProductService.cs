using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Services
{
    public class ProductService: ProductManagement.ProductManagementBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger, IProductService ProductService)
        {
            _logger = logger;
            _productService = ProductService;
        }

        public override Task<ProductResponse> AddProduct(Product newProduct, ServerCallContext context)
        {
            try
            {
                var newId = _productService.AddProduct(new Logic.Entities.Product() { Title = newProduct.Title,  Price = newProduct.Price, Count = newProduct.Count  });

                return Task.FromResult(new ProductResponse { Id = newId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddProduct failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<Products> GetProducts(Empty a, ServerCallContext context)
        {
            try
            {
                List<Logic.Entities.Product> Products = _productService.GetAll();

                var returnProducts = new Products();
                foreach (var newProduct in Products)
                {
                    returnProducts.ProductsList.Add(new Product() { Id = newProduct.Id, Title = newProduct.Title, Price = newProduct.Price, Count = newProduct.Count });
                }
                return Task.FromResult(returnProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetProducts failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<Empty> BuyProduct(BuyProductRequest buyProductRequest, ServerCallContext context)
        {
            try
            {
                _productService.BuyProduct(buyProductRequest.ProductId, buyProductRequest.UserId);
                return Task.FromResult(new Empty());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuyProduct failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<Product> GetProductById(ProductId ProductId, ServerCallContext context)
        {
            try
            {
                var product = _productService.GetProductById(ProductId.Id);

                var returnProduct = new Product { Id = product.Id, Count = product.Count, Price = product.Price, Title = product.Title };

                return Task.FromResult(returnProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetProductById failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<Empty> DeleteProduct(ProductId ProductId, ServerCallContext context)
        {
            try
            {
                _productService.DeleteProduct(ProductId.Id);

                return Task.FromResult(new Empty());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetProductById failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<ProductsUsers> GetProductUsers(ProductId productId, ServerCallContext context)
        {
            try
            {
                var productUsers = _productService.GetProductById(productId.Id);

                var resultUsers = new ProductsUsers();

                foreach(var user in productUsers.Users)
                {
                    resultUsers.UserList.Add(new User { Id = user.Id, Name = user.Name, Surname = user.Surname });
                }

                return Task.FromResult(resultUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetProductUsers failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }

        }
    }
}
