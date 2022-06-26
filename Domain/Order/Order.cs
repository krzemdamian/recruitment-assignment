using Common.Domain;
using Common.Domain.User.ValueObjects;
using Domain.Common;
using Domain.Order.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class Order : AggregateRoot<OrderId>
    {
        public UserId UserId { get; }

        private readonly ICollection<OrderItem> _items = new Collection<OrderItem>();
        private DiscountVoucher.ValueObjects.DiscountVoucherId _discountVoucherId;
        private Money _discountVoucherValue;
        private PaymentMethod _paymentMethod;
        private string _remarks;

        protected Order(OrderId id, UserId userId) : base(id)
        {
            this.UserId = userId;
        }

        public static Order CreateOrder(
            OrderId id,
            UserId userId,
            ICollection<OrderItem> items)
        {
            if (userId is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (items is null || !items.Any())
            {
                throw new ArgumentException("Order must contain at least one item.");
            }

            var result = new Order(id, userId);
            foreach(var item in items)
            {
                result._items.Add(item);
            }

            return result;
        }

        public void AddVoucher(DiscountVoucher.DiscountVoucher voucher)
        {
            if (voucher is null || voucher.IsUsed || !voucher.IsActive)
            {
                return;
            }

            _discountVoucherId = voucher.Id;
            _discountVoucherValue = voucher.Value;
        }

        public void AddRemarks(string remarks)
        {
            if (remarks is null)
            {
                return;
            }
            if (remarks.Length > 200)
            {
                throw new ArgumentOutOfRangeException("Comment cannot exceed 200 characters.");
            }

            this._remarks = remarks;
        }

        public void SetPaymentMethod(PaymentMethod paymentMethod)
        {
            if (paymentMethod == default)
            {
                throw new ArgumentException("Payment method must be specified.");
            }

            this._paymentMethod = paymentMethod;
        }
    }

    public enum PaymentMethod
    {
        NotSpecified,
        BankTransfer,
        CreditCard
    }
}
