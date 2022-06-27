using Domain.DiscountVoucher;
using Domain.Order.ValueObjects;
using Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class OrderFactory : IOrderFactory
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderFactory(
            IProductRepository productRepository,
            IOrderRepository orderRepository)

        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(ShoppingCart.ShoppingCart shoppingCart)
        {
            if (!shoppingCart.Items.Any())
            {
                throw new ApplicationException("Order cannot be created from empty shopping cart.");
            }

            var orderItems = await CreateOrderItemsFrom(shoppingCart);
            // DK: This is naive implementation because when exception occurs id continuity will be broken
            var nextId = await _orderRepository.GetNextSequanceAsync();
            var orderId = new OrderId(nextId);
            var order = Order.CreateOrder(orderId, shoppingCart.UserId, orderItems);

            shoppingCart.Clear();

            return order;
        }

        private async Task<ICollection<OrderItem>> CreateOrderItemsFrom(ShoppingCart.ShoppingCart shoppingCart)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in shoppingCart.Items)
            {
                var product = await _productRepository.GetOrNullAsync(item.ProductId);
                var orderItem = new OrderItem(product.Id, product.Price, item.Amount);
                orderItems.Add(orderItem);
            }

            return orderItems;
        }
    }
}
