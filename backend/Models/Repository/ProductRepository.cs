using backend.Models;
using backend.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context context)
        {
            this.context = context;
        }

        // Ajouter un produit
        public async Task<Product> AddProduct(Product product)
        {
            var result = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        // Supprimer un produit
        public async Task DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        // Obtenir tous les produits
        public async Task<List<Product>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        // Obtenir un produit par ID
        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }

        // Obtenir un produit par nom
        public async Task<Product> GetProductByName(string name)
        {
            return await context.Products
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        // Mettre à jour un produit
        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ImageUrl = product.ImageUrl;

                context.Products.Update(existingProduct);
                await context.SaveChangesAsync();
            }
        }
    }
}
