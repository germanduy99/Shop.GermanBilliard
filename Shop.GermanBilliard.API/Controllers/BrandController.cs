using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.GermanBilliard.Application.DTOs.Brand;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Commands;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Queries;

namespace Shop.GermanBilliard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandDto>>> GetAll()
        {
            var brands = await _mediator.Send(new GetBrandListRequest());
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> Get(int id)
        {
            var brand = await _mediator.Send(new GetBrandDetailRequest { Id = id });
            return Ok(brand);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandCommand request)
        {
            var respond = await _mediator.Send(request);
            return Ok(respond);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBrandCommand request)
        {
            var respond = await _mediator.Send(request);
            return Ok(respond);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBrandCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
