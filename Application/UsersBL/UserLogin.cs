using Domain.User;
using FluentValidation;
using Infrastructure.Dtos.UserDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersBL
{
    public class UserLogin
    {
        public class Command : IRequest<ServiceStatus<GetUserDto>>
        {
            public LoginDto Login { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login.Username).NotEmpty();
                RuleFor(x=>x.Login.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ServiceStatus<GetUserDto>>
        {
            private readonly UserManager<AppUsers> _userManager;
            private readonly TokenService _tokenService;
            private readonly ApplicationDbContext _context;

            public Handler( UserManager<AppUsers> userManager, TokenService tokenService, ApplicationDbContext context)
            {
                _tokenService = tokenService;
                _userManager = userManager;
                _context = context;
            }
            public async Task<ServiceStatus<GetUserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Login.Username);
                if (user == null || !await _userManager.CheckPasswordAsync(user, request.Login.Password))
                    return new ServiceStatus<GetUserDto>
                    {
                        Code = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Please Check the Username/Password",
                        Object = null
                    };

                var getuser = new GetUserDto
                {
                    Email = user.Email,
                    Token = await _tokenService.GenerateToken(user),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    RoleName= String.Join(",", _context.Roles.Where(y => y.Id == user.RoleId ).Select(y => y.Name)),
                    Id = user.Id
                };
                return new ServiceStatus<GetUserDto>
                {
                    Code = System.Net.HttpStatusCode.OK,
                    Message = "User LoggedIn Successfully",
                    Object = getuser
                };
                
                }
        }
    }
}
