using AutoMapper;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderBL
{
    public class Details
    {
        public class Query : IRequest<ServiceStatus<GetOrderDto>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<GetOrderDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<GetOrderDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Orders.Where(x => x.Id == request.Id).Include(x=>x.Driver).Include(x=>x.Vendor).ThenInclude(x=>x.VendorPrices).ThenInclude(x=>x.Prices).Include(x=>x.OrderHistory).ThenInclude(x=>x.OrderStatus).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<GetOrderDto>(result);
                    return new ServiceStatus<GetOrderDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<GetOrderDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
