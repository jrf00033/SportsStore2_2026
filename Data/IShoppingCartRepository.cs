using SportsStore2_2026.Models;

namespace SportsStore2_2026.Data
{
    public interface IShoppingCartRepository
    {
        void AddProductToCart(string cartID, int prodID);


        List<ShoppingCart> LoadCartItems(string cartID, out decimal total);

        void UpdateCartItem(string cartID, int prodID, int quantity);
    }
}
