﻿using AutoMapper;
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

namespace Application.DriverBL
{
    public class UpPaidOrderList
    {
        public class Query : IRequest<ServiceStatus<List<GetOrderDto>>>
        {
            public Guid DriverId { get; set; }
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
                var result = await _context.Orders.Where(x => x.DriverId == request.DriverId && x.DeliveryType == Domain.Orders.DeliveryType.COD && x.IsPaid == false).Include(x => x.Vendor).Include(x => x.Driver).OrderByDescending(x => x.CreatedDate).ToListAsync(cancellationToken);
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
