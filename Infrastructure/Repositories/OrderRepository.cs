using Domain.Order;
using Domain.Order.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, OrderId>, IOrderRepository
    {
        private int? nextIndex;
        public async ValueTask<int> GetNextSequanceAsync()
        {
            if (nextIndex is null)
            {
                nextIndex = await this.CountAsync();
            }
            nextIndex += 1;

            return nextIndex.Value;
        }
    }
}
