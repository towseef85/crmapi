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

namespace Application.OrderHistoryBL
{
    public class Create
    {
        public class Command : IRequest<ServiceStatus<Unit>>
        {
            public PostOrderHistoryDto OrderHistory { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.OrderHistory).SetValidator(new OrderHistoryValidation());
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
                    request.OrderHistory.Id = Guid.NewGuid();
                    request.OrderHistory.StatusUpdateDate= DateTime.Now;
                    _context.OrderHistories.Add(_mapper.Map<Domain.Orders.OrderHistory>(request.OrderHistory));
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = $"Order History Added Successfully!",
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
