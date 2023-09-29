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
    public class Details
    {
        public class Query : IRequest<ServiceStatus<GetOrderStatusShortDto>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<GetOrderStatusShortDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<GetOrderStatusShortDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.OrderStatuses.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<GetOrderStatusShortDto>(result);
                    return new ServiceStatus<GetOrderStatusShortDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<GetOrderStatusShortDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
