namespace backend.Models.Repository
{
    public interface ICartRepository
    {
        Cart GetCartByCustomerId(int customerId);
        void AddToCart(int customerId, int productId, int quantity);
        void RemoveFromCart(int customerId, int productId);
        void UpdateQuantity(int cartId, int productId, int quantity);
        void ClearCart(int customerId);
        IEnumerable<CartItem> GetCartItems(int customerId);
        void Save();
    }
}
