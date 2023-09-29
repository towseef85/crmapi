using FluentValidation;
using Infrastructure.Dtos.OrderHistoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderStatusBL
{
    public class OrderStatusValidation : AbstractValidator<PostOrderStatusDto>
    {
        public OrderStatusValidation()
        {

            RuleFor(x => x.EngName).NotEmpty();

        }
    }
}

