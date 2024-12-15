using backend.Models;
using backend.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // Récupérer le panier par CustomerId
        [HttpGet("{customerId}")]
        public IActionResult GetCart(int customerId)
        {
            var cart = _cartRepository.GetCartByCustomerId(customerId);
            if (cart == null)
                return NotFound("Panier non trouvé.");

            return Ok(cart);
        }

        // Ajouter un article au panier
        [HttpPost("{customerId}/add/{productId}/{quantity}")]
        public IActionResult AddToCart(int customerId, int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("La quantité doit être supérieure à zéro.");

            _cartRepository.AddToCart(customerId, productId, quantity);
            return Ok("Article ajouté au panier.");
        }

        // Supprimer un article du panier
        [HttpDelete("{customerId}/remove/{productId}")]
        public IActionResult RemoveFromCart(int customerId, int productId)
        {
            _cartRepository.RemoveFromCart(customerId, productId);
            return Ok("Article supprimé du panier.");
        }

        // Mettre à jour la quantité d'un article
        [HttpPut("{cartId}/update/{productId}/{quantity}")]
        public IActionResult UpdateQuantity(int cartId, int productId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("La quantité doit être supérieure à zéro.");

            _cartRepository.UpdateQuantity(cartId, productId, quantity);
            return Ok("Quantité mise à jour.");
        }

        // Vider le panier
        [HttpDelete("{customerId}/clear")]
        public IActionResult ClearCart(int customerId)
        {
            _cartRepository.ClearCart(customerId);
            return Ok("Panier vidé.");
        }

        // Récupérer les articles du panier
        [HttpGet("{customerId}/items")]
        public IActionResult GetCartItems(int customerId)
        {
            var items = _cartRepository.GetCartItems(customerId);
            return Ok(items);
        }
    }
}
