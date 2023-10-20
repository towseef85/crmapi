using FluentValidation;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersBL
{
    public class RegisterUserValidation : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}