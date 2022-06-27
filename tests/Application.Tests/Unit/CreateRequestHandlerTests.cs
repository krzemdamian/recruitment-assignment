using Application.Order.Create;
using Common.Domain.User.ValueObjects;
using Domain.DiscountVoucher;
using Domain.Order;
using Domain.ShoppingCart;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class CreateRequestHandlerTests
    {
        private CreateRequestHandler _sut;
        private Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
        private Mock<IOrderFactory> _orderFactoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IDiscountVoucherRepository> _discountVoucherRepositoryMock;

        public CreateRequestHandlerTests()
        {
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _orderFactoryMock = new Mock<IOrderFactory>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _discountVoucherRepositoryMock = new Mock<IDiscountVoucherRepository>();
            _sut = new CreateRequestHandler(
                _shoppingCartRepositoryMock.Object,
                _orderFactoryMock.Object,
                _orderRepositoryMock.Object,
                _discountVoucherRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_CreatesOrder()
        {
            // Arrange
            var userId = new UserId();
            var shoppingCart = new Domain.ShoppingCart.ShoppingCart(userId);
            var productId = new Domain.Product.ValueObjects.ProductId();
            shoppingCart.AddProduct(productId);

            _shoppingCartRepositoryMock
                .Setup(m => m.GetOrNullAsync(It.IsAny<Domain.ShoppingCart.ValueObjects.ShoppingCartId>()))
                .Returns(ValueTask.FromResult(shoppingCart));

            var orderItem = new OrderItem(productId, new Domain.Common.Money(10), new Domain.Common.Amount(2));
            var order = Domain.Order.Order.CreateOrder(
                new Domain.Order.ValueObjects.OrderId(1),
                userId, new List<OrderItem>() { orderItem });

            _orderFactoryMock.Setup(f => f.CreateOrder(It.IsAny<Domain.ShoppingCart.ShoppingCart>())).Returns(Task.FromResult(order));

            const string remarks = "test_remark";
            var request = new CreateRequest(
                shoppingCart.Id,
                Order.Create.PaymentMethod.CreditCard,
                remarks);

            // Act
            await _sut.Handle(request, CancellationToken.None);

            // Assert
            _orderFactoryMock.Verify(m => m.CreateOrder(shoppingCart), Times.Once);
        }
    }
}