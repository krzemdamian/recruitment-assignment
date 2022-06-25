using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Order.Create
{
    public class CreateRequest : IRequest<Guid>
    {
        public Guid ShoppingCartId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Remarks { get; set; }

        public CreateRequest(Guid shoppingCartId, PaymentMethod paymentMethod, string remarks)
        {
            ShoppingCartId = shoppingCartId;
            PaymentMethod = paymentMethod;
            Remarks = remarks;
        }
    }

    // Note DK: It's better to use ClassEnums also called SmartEnums
    // Smart enumes can encapsulate converters, representations methods, etc.
    // Ref from MSDN: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    public enum PaymentMethod
    {
        BankTransfer,
        CreditCard
    }
}
