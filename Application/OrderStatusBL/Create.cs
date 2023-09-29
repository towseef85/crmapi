using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.OrderHistoryDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;

namespace Application.OrderStatusBL
{
    public class Create
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
                    var getName = _context.OrderStatuses.Where(x=>x.EngName == request.OrderStatus.EngName).Any();
                    if(getName)
                    {
                        return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.Conflict,
                            Message = $"Order status by name {request.OrderStatus.EngName} already Exists!",
                            Object = Unit.Value
                        };
                    }
                    request.OrderStatus.Id = Guid.NewGuid();
                    _context.OrderStatuses.Add(_mapper.Map<Domain.Orders.OrderStatus>(request.OrderStatus));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Order Status Added Successfully!",
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
