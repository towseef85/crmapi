using Application.DriverPaymentBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.DriverpaymentDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverPaymentHeadPaymentBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<PostDriverPaymentHeadDto>>
        {
            public PostDriverPaymentHeadDto DriverPaymentHead { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DriverPaymentHead).SetValidator(new DriverPaymentHeadValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<PostDriverPaymentHeadDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PostDriverPaymentHeadDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    request.DriverPaymentHead.Id = Guid.NewGuid();
                    _context.DriverPaymentHeads.Add(_mapper.Map<Domain.Drivers.DriverPaymentHead>(request.DriverPaymentHead));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if(result)
                    {
                       var orderId=   request.DriverPaymentHead.PaymentDetails.Select(x => x.OrderId).ToList();
                        var orders = await _context.Orders.Where(x => orderId.Contains(x.Id)).ToListAsync();
                        foreach(var item in orders)
                        {
                            item.IsPaid = true;
                        }
                        await _context.SaveChangesAsync(cancellationToken);
                     
                    }
                    return new ServiceStatus<PostDriverPaymentHeadDto>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Driver Payment Added Successfully!",
                        Object = request.DriverPaymentHead
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostDriverPaymentHeadDto>
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
