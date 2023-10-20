using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public class AppUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }
        public string? Remarks { get; set; }


    }
}
