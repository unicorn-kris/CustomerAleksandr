using Repository.Entities;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        int AddProduct(Product product);

        void DeleteProduct(int productId);

        void BuyProduct(int productId, int userId);

        List<Product> GetAll();

    }
}
