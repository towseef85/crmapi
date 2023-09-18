using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverBL
{
    public class DriverValidation : AbstractValidator<PostDriverDto>
    {
        public DriverValidation()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MobileNo).NotEmpty();
            RuleFor(x => x.WorkType).Must((x => x == "FullTime" || x == "PartTime"));
            RuleFor(x => x.Status).Must((x => x == "Active" || x == "InActive"));
        }
    }

}

