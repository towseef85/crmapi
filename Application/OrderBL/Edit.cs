using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderBL
{
    public class Edit
    {
 
            public class Command : IRequest<ServiceStatus<Unit>>
            {
                public PostOrderDto Order { get; set; }
            }
            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Order).SetValidator(new OrderValidation());
                }
            }
            public class Handler : IRequestHandler<Command, ServiceStatus<Unit>>
            {
                private readonly ApplicationDbContext _context;
                private readonly IMapper _mapper;

                public Handler(ApplicationDbContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;

                }
                public async Task<ServiceStatus<Unit>> Handle(Command request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var res = _context.Orders.FirstOrDefault(x => x.Id == request.Order.Id);
                        if (res != null)
                        {
                            _mapper.Map(request.Order, res);
                            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                            return new ServiceStatus<Unit>
                            {
                                Code = System.Net.HttpStatusCode.OK,
                                Message = $"Order Updated Successfully!",
                                Object = Unit.Value
                            };
                        }
                        return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.NotFound,
                            Message = $"Record Not Found!",
                            Object = Unit.Value
                        };
                    }
                    catch (Exception ex)
                    {
                        Exception exception = ex;

                        return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.InternalServerError,
                            Message = ex.Message.ToString(),
                            InnerMessage = exception.InnerException != null ? exception.InnerException.ToString() : "",
                            Object = Unit.Value
                        };
                    }
                }
            }
        }
    
}
