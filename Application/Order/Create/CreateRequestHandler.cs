using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.DiscountVoucher;
using Domain.Order;
using Domain.ShoppingCart;
using Domain.ShoppingCart.ValueObjects;
using MediatR;

namespace Application.Order.Create
{
    public class CreateRequestHandler : IRequestHandler<CreateRequest, int>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderFactory _orderFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountVoucherRepository _discountVoucherRepository;

        public CreateRequestHandler(
            IShoppingCartRepository shoppingCartRepository,
            IOrderFactory orderFactory,
            IOrderRepository orderRepository,
            IDiscountVoucherRepository discountVoucherRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _discountVoucherRepository = discountVoucherRepository;
        }
        public async Task<int> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var shoppingCartId = new ShoppingCartId(request.ShoppingCartId);
            var shoppingCart = await _shoppingCartRepository.GetOrNullAsync(shoppingCartId);
            Domain.Order.PaymentMethod paymentMethod = this.MapPaymentMethod(request.PaymentMethod);
            var order = await _orderFactory.CreateOrder(shoppingCart);
            order.SetPaymentMethod(paymentMethod);
            // DK TODO: update shoppingCart in repository

            var discountVoucher = await _discountVoucherRepository.GetOrNullAsync(shoppingCart.DiscountVoucherId);
            discountVoucher?.UseOn(order);
            // DK TODO: update discountVoucer in repository

            order.AddRemarks(request.Remarks);

            await _orderRepository.InsertAsync(order);
            return order.Id.Value;
        }

        // DK Note: Extension method or mapper can be used
        private Domain.Order.PaymentMethod MapPaymentMethod(PaymentMethod requestPaymentMethod)
            => requestPaymentMethod switch
            {
                PaymentMethod.CreditCard => Domain.Order.PaymentMethod.CreditCard,
                PaymentMethod.BankTransfer => Domain.Order.PaymentMethod.BankTransfer,
                _ => Domain.Order.PaymentMethod.NotSpecified
            };
    }
}
