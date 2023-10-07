using FluentValidation;
using Infrastructure.Dtos.OrderRequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderRequestBL
{
    public class OrderRequestValidation : AbstractValidator<PostOrderRequestDto>
    {
        public OrderRequestValidation()
        {

            RuleFor(x => x.VendorId).NotEmpty();
            RuleFor(x=>x.DeliveryDate).NotEmpty();
            RuleFor(x=>x.DeliveryType).NotNull();

        }
    }
}
