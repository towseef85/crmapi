using Infrastructure.Dtos.UserDto;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersBL
{
    public class RoleList
    {
        public class Query : IRequest<ServiceStatus<List<GetRoleDto>>> { }

        public class Handler : IRequestHandler<Query, ServiceStatus<List<GetRoleDto>>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {

                _roleManager = roleManager;
            }

            public async Task<ServiceStatus<List<GetRoleDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roleList = await _roleManager.Roles.ToListAsync();
                GetRoleDto[] roles = new GetRoleDto[] { };
                var getlist = roles.ToList();
                if (roleList != null)
                {

                    foreach (var role in roleList)
                    {
                        var rolelist = new GetRoleDto
                        {
                            Id = role.Id,
                            Name = role.Name,
                        };
                        getlist.Add(rolelist);

                    }

                }

                return new ServiceStatus<List<GetRoleDto>>
                {
                    Code = System.Net.HttpStatusCode.OK,
                    Message = "Roles Fetch Successfully!",
                    Object = getlist
                };

            }
        }
    }
}
