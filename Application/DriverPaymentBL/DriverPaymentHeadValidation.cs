using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.DriverpaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverPaymentBL
{
    public class DriverPaymentHeadValidation : AbstractValidator<PostDriverPaymentHeadDto>
    {
        public DriverPaymentHeadValidation()
        {

            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.TotalAmount).NotEmpty();
            RuleFor(x => x.PaidAmount).Equal(x=>x.TotalAmount);
            RuleFor(x => x.PaymentDetails).NotNull().NotEmpty();
        }
    }

}
