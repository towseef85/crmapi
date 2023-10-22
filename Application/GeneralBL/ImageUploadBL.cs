using Infrastructure.Dtos.GeneralDto;
using Infrastructure.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GeneralBL
{
    public class ImageUploadBL 
    {
        public class Commad : IRequest<ServiceStatus<ImageResponse>>
        {
            public PostImageDto imageDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Order).SetValidator(new OrderValidation());
            }
        }
    }
}
