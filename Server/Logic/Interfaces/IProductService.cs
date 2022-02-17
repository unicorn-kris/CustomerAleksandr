using Logic.Entities;
using System.Collections.Generic;

namespace Logic.Interfaces
{
    public interface IProductService
    {
        int AddProduct(Product product);

        void DeleteProduct(int productId);

        void BuyProduct(int productId, int userId);

        List<Product> GetAll();

        Product GetProductById(int productId);
    }
}
