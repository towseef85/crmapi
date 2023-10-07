using Application.OrderRequestBL;
using Infrastructure.Dtos.OrderRequestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
 
    public class OrderRequestController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderRequestDto))]
        public async Task<IActionResult> GetList()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid Id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = Id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostOrderRequestDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(PostOrderRequestDto OrderRequest)
        {
            return HandleResult(await Mediator.Send(new Create.Command { OrderRequest = OrderRequest }));
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostOrderRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(PostOrderRequestDto OrderRequest)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { OrderRequest = OrderRequest }));
        }
    }
}
