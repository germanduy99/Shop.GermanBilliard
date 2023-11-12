using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Commands;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Queries;
using Shop.GermanBilliard.Application.Features.Order.Requests.Commands;
using Shop.GermanBilliard.Application.Features.Order.Requests.Queries;

namespace Shop.GermanBilliard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            var orders = await _mediator.Send(new GetOrderListRequest());
            return Ok(orders);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CueDto>> Get(int id)
        {
            var order = await _mediator.Send(new GetOrderDetailRequest { Id = id });
            return Ok(order);
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand request)
        {
            var respond = await _mediator.Send(request);
            return Ok(respond);
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderCommand request)
        {
            var respond = await _mediator.Send(request);
            return Ok(respond);
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
