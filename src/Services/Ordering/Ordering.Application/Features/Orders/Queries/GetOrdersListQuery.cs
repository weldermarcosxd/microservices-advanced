using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Ordering.Application.Features.Orders.Queries
{
    public class GetOrdersListQuery : IRequest<IList<OrderDto>>
    {
        public string Username { get; set; }

        public GetOrdersListQuery(string userName)
        {
            Username = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }

    public class GetOrdersListQuery : IRequestHandler<GetOrdersListQuery, IList<OrderDto>>
    {
        public Task<IList<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
