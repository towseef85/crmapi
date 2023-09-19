using AutoMapper;
using Infrastructure.Providers;
using MediatR;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverBL
{
    public class Delete
    {
        public class Command : IRequest<ServiceStatus<Unit>>
        {
            public Guid Id { get; set; }
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
                    var res = _context.Drivers.FirstOrDefault(x => x.Id == request.Id);
                    if(res != null)
                    {
                        res.Deleted = true;
                        res.DeleteDate = DateTime.Now;
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = $"Driver Deleted Successfully!",
                            Object = Unit.Value
                        };
                    }
                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = "Record Not Found",
                        
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
