﻿using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.VendorPriceDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VendorPricesBL
{
    public class Update
    {
        public class Command : IRequest<ServiceStatus<PostVendorPriceDto>>
        {
            public PostVendorPriceDto VendorPrice { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.VendorPrice).SetValidator(new VendorPriceValidation());
            }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<PostVendorPriceDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PostVendorPriceDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.VendorPrices.FirstOrDefault(x => x.Id == request.VendorPrice.Id);
                    if (res != null)
                    {
                        _mapper.Map(request.VendorPrice, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<PostVendorPriceDto>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"Vendor Price Updated Successfully!",
                            Object = request.VendorPrice
                        };
                    }
                    return new ServiceStatus<PostVendorPriceDto>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
                        Object = request.VendorPrice
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostVendorPriceDto>
                    {
                        Code = System.Net.HttpStatusCode.InternalServerError,
                        Message = ex.Message.ToString(),
                        InnerMessage = exception.InnerException != null ? exception.InnerException.ToString() : "",
                        Object = null
                    };
                }
            }
        }
    }
}
