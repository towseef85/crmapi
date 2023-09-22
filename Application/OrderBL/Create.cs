using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderBL
{
    public class Create
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
                    //request.Order.CreatedUserId = new Guid(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    Guid orderId = Guid.NewGuid();
                    request.Order.Id = orderId;

                    _context.Orders.Add(_mapper.Map<Domain.Orders.Order>(request.Order));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (result)
                    {
                        var getStatus = await _context.OrderStatuses.Where(x => x.EngName == "Created").FirstOrDefaultAsync();
                        var orderHistory = new Domain.Orders.OrderHistory
                        {
                            Id = Guid.NewGuid(),
                            OrderId = orderId,
                            StatusId = getStatus.Id,
                            Remarks = "Order Created",
                            StatusUpdateDate = DateTime.Now.Date
                        };
                        _context.OrderHistories.Add(orderHistory);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Order Added Successfully!",
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
