using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Order.Create
{
    public class CreateRequestHandler : IRequestHandler<CreateRequest, Guid>
    {
        public Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
