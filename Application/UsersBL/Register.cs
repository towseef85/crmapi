using Application.DriverBL;
using AutoMapper;
using Domain.User;
using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.UserDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersBL
{
    public class Register 
    {
        public class Command : IRequest<ServiceStatus<Unit>>
        {
            public RegisterUserDto UserDto { get; set; }
        }
      

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserDto).SetValidator(new RegisterUserValidation());
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<Unit>>
        {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<AppUsers> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(ApplicationDbContext context, UserManager<AppUsers> userManager, RoleManager<IdentityRole> roleManager)
            {
                _context = context;
                _userManager = userManager;
                _roleManager = roleManager;

            }
            public async Task<ServiceStatus<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = new AppUsers
                    {
                        UserName = request.UserDto.UserName,
                        Email = request.UserDto.Email,
                        FirstName = request.UserDto.FirstName,
                        LastName = request.UserDto.LastName,
                        RoleId = request.UserDto.RoleId,
                        IsActive = request.UserDto.IsActive,
                    };
                    IdentityResult result = await _userManager.CreateAsync(user, request.UserDto.Password);
                    if(result.Succeeded)
                    {
                      var roleresult = await _roleManager.FindByIdAsync(request.UserDto.RoleId);
                        await _userManager.AddToRoleAsync(user, roleresult.Name);

                       
                    }

                    return new ServiceStatus<Unit>
                    {
                        Code = System.Net.HttpStatusCode.OK,
                        Message = "User Created Successfully",
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
