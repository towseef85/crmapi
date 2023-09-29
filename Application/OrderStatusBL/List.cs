using AutoMapper;
using Infrastructure.Dtos.OrderHistoryDto;
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

namespace Application.OrderStatusBL
{
  public class List
    {
        public class Query : IRequest<ServiceStatus<List<GetOrderStatusShortDto>>>
        {
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetOrderStatusShortDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<List<GetOrderStatusShortDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.OrderStatuses.Where(x => x.EngName != "Created").ToListAsync(cancellationToken);
                if (result != null)
                {
                    return new ServiceStatus<List<GetOrderStatusShortDto>>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = _mapper.Map<List<GetOrderStatusShortDto>>(result)
                    };
                }
                return new ServiceStatus<List<GetOrderStatusShortDto>>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
