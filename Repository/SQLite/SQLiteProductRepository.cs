using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Exceptions;
using Repository.Interfaces;
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

                using (var db = new ShopContext())
                {
                    db.Add(product);
                    db.SaveChanges();

                    productId = product.Id;
                }
                return productId;
            }
            catch
            {
                throw new ProductRepositoryException("");
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
            catch
            {
                throw new ProductRepositoryException();
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
            catch
            {
                throw new ProductRepositoryException();
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                var products = new List<Product>();

                using (ShopContext db = new ShopContext())
                {
                    foreach (var product in db.Products)
                    {
                        products.Add(product);
                    }
                }
                return products;
            }
            catch
            {
                throw new ProductRepositoryException();
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                var newProduct = new Product();

                using (ShopContext db = new ShopContext())
                {
                    newProduct = db.Products.FirstOrDefault(p => p.Id == productId);
                }

                return newProduct;
            }
            catch
            {
                throw new ProductRepositoryException();
            }
        }
    }
}
