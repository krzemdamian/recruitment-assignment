using System.Threading.Tasks;

namespace Domain.Order
{
    public interface IOrderFactory
    {
        Task<Order> CreateOrder(ShoppingCart.ShoppingCart shoppingCart);
    }
}