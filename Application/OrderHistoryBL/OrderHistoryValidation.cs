using FluentValidation;
using Infrastructure.Dtos.OrderHistoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderHistoryBL
{
    public class OrderHistoryValidation : AbstractValidator<PostOrderHistoryDto>
    {
        public OrderHistoryValidation()
        {

            RuleFor(x => x.StatusId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();

        }
    }
}
