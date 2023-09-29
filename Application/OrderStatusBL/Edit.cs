using Application.OrderStatusBL;
using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.OrderHistoryDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderStatusBL
{
    public class Edit
    {
        public class Command : IRequest<ServiceStatus<Unit>>
        {
            public PostOrderStatusDto OrderStatus { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.OrderStatus).SetValidator(new OrderStatusValidation());
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
                    var res = _context.OrderStatuses.FirstOrDefault(x => x.Id == request.OrderStatus.Id);
                    var getName = _context.OrderStatuses.Where(x => x.EngName == request.OrderStatus.EngName && x.Id != request.OrderStatus.Id).Any();
                    if (getName)
                    {
                        return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.Conflict,
                            Message = $"Order status by name {request.OrderStatus.EngName} already Exists!",
                            Object = Unit.Value
                        };
                    }
                    if (res != null)
                    {
                        _mapper.Map(request.OrderStatus, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"OrderStatus Updated Successfully!",
                            Object = Unit.Value
                        };
                    }
                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
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
