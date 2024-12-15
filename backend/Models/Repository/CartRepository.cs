using backend.Models;
using backend.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;

        public CartRepository(Context context)
        {
            _context = context;
        }

        // Récupérer le panier d'un client par CustomerId
        public Cart GetCartByCustomerId(int customerId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.CustomerId == customerId);
        }

        // Ajouter un produit au panier
        public void AddToCart(int customerId, int productId, int quantity)
        {
            var cart = GetOrCreateCart(customerId);

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            Save();
        }

        // Supprimer un produit du panier
        public void RemoveFromCart(int customerId, int productId)
        {
            var cart = GetCartByCustomerId(customerId);
            if (cart == null) return;

            var cartItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                Save();
            }
        }

        // Mettre à jour la quantité d'un produit dans le panier
        public void UpdateQuantity(int cartId, int productId, int quantity)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.Id == cartId);

            var cartItem = cart?.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                Save();
            }
        }

        // Supprimer tous les articles d'un panier
        public void ClearCart(int customerId)
        {
            var cart = GetCartByCustomerId(customerId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                Save();
            }
        }

        // Récupérer les articles d'un panier
        public IEnumerable<CartItem> GetCartItems(int customerId)
        {
            var cart = GetCartByCustomerId(customerId);
            return cart?.CartItems ?? new List<CartItem>();
        }

        // Sauvegarder les modifications
        public void Save()
        {
            _context.SaveChanges();
        }

        // Méthode pour obtenir ou créer un panier
        private Cart GetOrCreateCart(int customerId)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                _context.Carts.Add(cart);
                Save();
            }

            return cart;
        }
    }
}
