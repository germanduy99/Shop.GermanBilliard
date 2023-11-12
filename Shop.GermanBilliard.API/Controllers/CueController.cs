using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Commands;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Queries;


namespace Shop.GermanBilliard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CueController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CueDto>>> Get()
        {
            var cues = await _mediator.Send(new GetCueListRequest());
            return Ok(cues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CueDto>> Get(int id)
        {
            var cue = await _mediator.Send(new GetCueDetailRequest { Id = id });
            return Ok(cue);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCueCommand request)
        {
            var respond = await _mediator.Send(request);
            return Ok(respond);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCueCommand request)
        {
            var respond = await _mediator.Send(request);
            return Ok(respond);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCueCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

    }
}
