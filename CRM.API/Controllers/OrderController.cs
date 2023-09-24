using Application.OrderBL;
using Infrastructure.Dtos.OrderDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
   

    public class OrderController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderDto))]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid Id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = Id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostOrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostOrderDto order)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Order = order }));
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostOrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(PostOrderDto order)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { Order = order }));
        }
    }
}
