using backend.Models;
using backend.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // Récupérer tous les produits
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await repository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur est survenue : {ex.Message}");
            }
        }


        // Récupérer un produit par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await repository.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Produit avec l'ID {id} introuvable.");
            }
            return Ok(product);
        }

        // Ajouter un nouveau produit
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var createdProduct = await repository.AddProduct(product);
            if (createdProduct == null)
            {
                return BadRequest("Un problème est survenu lors de l'ajout du produit.");
            }

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        // Mettre à jour un produit
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("L'ID du produit dans l'URL ne correspond pas à celui des données.");
            }

            await repository.UpdateProduct(product);
            return NoContent();
        }

        // Supprimer un produit
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await repository.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Produit avec l'ID {id} introuvable.");
            }

            await repository.DeleteProduct(id);
            return NoContent();
        }
    }
}
