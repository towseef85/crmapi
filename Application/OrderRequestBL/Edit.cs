using AutoMapper;
using FluentValidation;
using Infrastructure.Dtos.OrderRequestDto;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderRequestBL
{
    public class Edit
    {
        public class Command : IRequest<ServiceStatus<PostOrderRequestDto>>
        {
            public PostOrderRequestDto OrderRequest { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.OrderRequest).SetValidator(new OrderRequestValidation());
            }
        }
        public class Handler : IRequestHandler<Command, ServiceStatus<PostOrderRequestDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<ServiceStatus<PostOrderRequestDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var res = _context.OrderRequests.FirstOrDefault(x => x.Id == request.OrderRequest.Id);
                    if (res != null)
                    {
                        _mapper.Map(request.OrderRequest, res);
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<PostOrderRequestDto>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"OrderRequest Updated Successfully!",
                            Object = request.OrderRequest
                        };
                    }
                    return new ServiceStatus<PostOrderRequestDto>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = $"Id Not Found!",
                        Object = request.OrderRequest
                    };
                }
                catch (Exception ex)
                {
                    Exception exception = ex;

                    return new ServiceStatus<PostOrderRequestDto>
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
