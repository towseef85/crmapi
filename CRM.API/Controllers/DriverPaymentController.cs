using Application.DriverPaymentHeadPaymentBL;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.DriverpaymentDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{

    public class DriverPaymentController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostDriverPaymentHeadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostDriverPaymentHeadDto DriverPaymentHead)
        {
            return HandleResult(await Mediator.Send(new Create.Command { DriverPaymentHead = DriverPaymentHead }));
        }
    }
}
