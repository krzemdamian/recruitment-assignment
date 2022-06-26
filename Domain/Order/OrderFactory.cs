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
    public class OrderFactory
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountVoucherRepository _discountVoucherRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderFactory(
            IProductRepository productRepository,
            IDiscountVoucherRepository discountVoucherRepository,
            IOrderRepository orderRepository)

        {
            _productRepository = productRepository;
            _discountVoucherRepository = discountVoucherRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(ShoppingCart.ShoppingCart shoppingCart, PaymentMethod paymentMethod)
        {
            if (!shoppingCart.Items.Any())
            {
                throw new ApplicationException("Order cannot be created from empty shopping cart.");
            }

            var orderItems = await CreateOrderItemsFrom(shoppingCart);
            // DK: This is naive implementation because when exception occurs id continuity will be broken
            var nextId = _orderRepository.GetNextSequance();
            var orderId = new OrderId(nextId);
            var order = Order.CreateOrder(orderId, shoppingCart.UserId, orderItems);
            order.SetPaymentMethod(paymentMethod);

            var discountVoucher = await _discountVoucherRepository.GetOrNullAsync(shoppingCart.DiscountVoucherId);
            discountVoucher?.UseOn(order);

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
