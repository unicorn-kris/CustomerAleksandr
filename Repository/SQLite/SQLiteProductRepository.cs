using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Exceptions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.SQLite
{
    internal class SQLiteProductRepository : IProductRepository
    {
        public int AddProduct(Product product)
        {
            try
            {
                int productId = 0;

                using (ShopContext db = new ShopContext())
                {
                    db.Add(product);
                    db.SaveChanges();

                    productId = product.Id;
                }
                return productId;
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
                using (var db = new ShopContext())
                {
                    var product = db.Products
                        .FirstOrDefault(p => p.Id == productId);

                    var user = db.Users
                        .FirstOrDefault(u => u.Id == userId);

                    if (product.Count != 0)
                    {
                        product.Count -= 1;
                        user.Products.Add(product);
                        db.SaveChanges();
                    }
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
                using (var db = new ShopContext())
                {
                    var product = db.Products
                        .Include(c => c.Users)
                        .FirstOrDefault(p => p.Id == productId);

                    if (product != null)
                    {
                        db.Remove(product);
                        db.SaveChanges();
                    }
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
                using (ShopContext db = new ShopContext())
                {
                    return db.Products.ToList();
                }
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
                using (ShopContext db = new ShopContext())
                {
                    return db.Products.FirstOrDefault(p => p.Id == productId);
                }
            }
            catch (Exception ex)
            {
                throw new ProductRepositoryException("GetProductById failed ", ex);
            }
        }
    }
}
