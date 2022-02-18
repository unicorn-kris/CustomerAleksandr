using Logic.Entities;
using Logic.Exceptions;
using Logic.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Logic.Services
{
    internal class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository data)
        {
            _productRepository = data;
        }

        public int AddProduct(Product product)
        {
            try
            {
                var newproduct = new Repository.Entities.Product();
                newproduct.Title = product.Title;
                newproduct.Price = product.Price;
                newproduct.Count = product.Count;

                return _productRepository.AddProduct(newproduct);
            }
            catch (Exception ex)
            {
                throw new ProductLogicException("GetAll failed ", ex);
            }
        }

        public void BuyProduct(int productId, int userId)
        {
            try
            {
                _productRepository.BuyProduct(productId, userId);
            }
            catch (Exception ex)
            {
                throw new ProductLogicException("BuyProduct failed ", ex);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                _productRepository.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                throw new ProductLogicException("DeleteProduct failed ", ex);
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                var result = new List<Product>();

                var products = _productRepository.GetAll();

                foreach (var product in products)
                {
                    var newProduct = new Product
                    {
                        Id = product.Id,
                        Count = product.Count,
                        Price = product.Price,
                        Title = product.Title
                    };

                    foreach (var user in product.Users)
                    {
                        newProduct.Users.Add(new User { Id = user.Id, Name = user.Name, Surname = user.Surname });
                    }
                    result.Add(newProduct);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ProductLogicException("GetAll failed ", ex);
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                var product = _productRepository.GetProductById(productId);

                return new Product { Id = product.Id, Count = product.Count, Price = product.Price, Title = product.Title };
            }
            catch (Exception ex)
            {
                throw new ProductLogicException("GetAll failed ", ex);
            }
        }
    }
}
