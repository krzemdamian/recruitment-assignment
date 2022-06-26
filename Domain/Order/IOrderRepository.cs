using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using Common.Domain.User.ValueObjects;
using Domain.Order.ValueObjects;

namespace Domain.Order
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        ValueTask<int> GetNextSequanceAsync();
    }
}
