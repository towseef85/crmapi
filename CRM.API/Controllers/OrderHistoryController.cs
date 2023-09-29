using Application.OrderHistoryBL;
using Infrastructure.Dtos.OrderHistoryDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{

    public class OrderHistoryController : BaseApiController
    {
        [HttpGet("GetOrderStatusList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderStatusShortDto))]
        public async Task<IActionResult> GetOrderStatusList()
        {
            return HandleResult(await Mediator.Send(new OrderStatusList.Query()));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostOrderHistoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostOrderHistoryDto OrderHistory)
        {
            return HandleResult(await Mediator.Send(new Create.Command { OrderHistory = OrderHistory }));
        }
    }
}
