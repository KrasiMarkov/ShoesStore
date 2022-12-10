using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoesStore.Data.Models;
using ShoesStore.Services.Shoes;

namespace ShoesStore.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        void AddToCart(ShoeServiceModel shoe);

        int RemoveFromCart(int id);

        void EmptyCart();

        List<Cart> GetCartItems();

        string GetItem(int id);

        int GetCount();

        decimal GetTotal();

        int CreateOrder(Order order);

    }
}
