﻿using AutoMapper;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System.Net;

namespace Application.OrderBL
{
    public class List
    {
        public class Query : IRequest<ServiceStatus<List<GetOrderDto>>>
        {
        }
        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetOrderDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<List<GetOrderDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Orders.Include(x=>x.Driver).Include(x=>x.Vendor).Include(x=>x.OrderHistory).ThenInclude(x=>x.OrderStatus).OrderBy(x=>x.CreatedDate).ToListAsync(cancellationToken);
                if (result != null)
                {
                    var list = _mapper.Map<List<GetOrderDto>>(result);
                    return new ServiceStatus<List<GetOrderDto>>
                    {
                        Code = HttpStatusCode.OK,
                        Message = "Data Fetch Successfully",
                        Object = list
                    };
                }
                return new ServiceStatus<List<GetOrderDto>>
                {
                    Code = HttpStatusCode.NotFound,
                    Message = "Data Not Found",
                    Object = null
                };
            }
        }
    }
}
