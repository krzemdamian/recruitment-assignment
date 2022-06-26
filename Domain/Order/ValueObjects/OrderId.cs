using Common.Domain;
using Common.Domain.ValueObjects.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order.ValueObjects
{
    public class OrderId : IntegerIdValueObject
    {
        public static OrderId Void { get; } = new OrderId(0);

        public OrderId() : base(0)
        {
        }

        public OrderId(int id) : base(id)
        {
        }

        public static OrderId CreateOrNull(int? id) => id is null ? Void : new OrderId(id.Value);

    }
}
