using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Exceptions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RepositoryTests")]

namespace Repository.SQLite
{
    internal class SQLiteProductRepository : IProductRepository
    {
        private IContext _context;

        public SQLiteProductRepository(IContext context)
        {
            _context = context;
        }

        public int AddProduct(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();

                return product.Id;
            }
            catch (Exception ex)
            {
                throw new ProductRepositoryException("AddProduct failed ", ex);
            }
        }

        public void BuyProduct(int productId, int userId)
        {
            try
            {
                var product = _context.Products
                    .FirstOrDefault(p => p.Id == productId);

                var user = _context.Users
                    .FirstOrDefault(u => u.Id == userId);

                if (product.Count != 0)
                {
                    product.Count -= 1;
                    user.Products.Add(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ProductRepositoryException("BuyProduct failed ", ex);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                var product = _context.Products
                    .Include(c => c.Users)
                    .FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    _context.Remove(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ProductRepositoryException("DeleteProduct failed ", ex);
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new ProductRepositoryException("GetAll failed ", ex);
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                return _context.Products
                                .FirstOrDefault(p => p.Id == productId);
            }
            catch (Exception ex)
            {
                throw new ProductRepositoryException("GetProductById failed ", ex);
            }
        }
    }
}
