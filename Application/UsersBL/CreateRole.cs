using FluentValidation;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersBL
{
    public class CreateRole
    {
        public class Command : IRequest<ServiceStatus<Unit>>
        {
            public string Role { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Role).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<Unit>>
        {

            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {

                _roleManager = roleManager;
            }
            public async Task<ServiceStatus<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var roleExists = await _roleManager.RoleExistsAsync(request.Role);
                    if (!roleExists)
                    {
                        IdentityResult roleResult = await _roleManager.CreateAsync(new IdentityRole(request.Role));
                        if (roleResult.Succeeded) return new ServiceStatus<Unit>
                        {
                            Code = System.Net.HttpStatusCode.OK,
                            Message = "Role Created Successfully",
                            Object = Unit.Value
                        };
                    }
                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.Conflict,
                        Message = "Role Name Already exists",
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
