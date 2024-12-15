using backend.Models;

namespace backend.Models.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> AddProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
