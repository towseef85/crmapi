using AutoMapper;
using Infrastructure.Dtos.OrderRequestDto;
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

namespace Application.OrderRequestBL
{
    public class Details
    {
        public class Query : IRequest<ServiceStatus<GetOrderRequestDto>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<GetOrderRequestDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<GetOrderRequestDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.OrderRequests.Include(x=>x.Vendor).Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (result != null)
                {
                    return new ServiceStatus<GetOrderRequestDto>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = _mapper.Map<GetOrderRequestDto>(result)
                };
                }
                return new ServiceStatus<GetOrderRequestDto>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
