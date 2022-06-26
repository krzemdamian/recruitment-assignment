using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using Domain.Common;
using Domain.Product.ValueObjects;
using Domain.Order.ValueObjects;

namespace Domain.Order
{
    public class OrderItem : Entity<ItemId>
    {
        public ProductId ProductId { get; }
        public Money ProductPrice { get; }
        public Amount Amount { get; }

        public OrderItem(ProductId productId, Money productPrice, Amount amount)
        {
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            ProductPrice = productPrice ?? throw new ArgumentNullException(nameof(productId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }
    }
}
