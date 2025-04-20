using BE.Application.ImmobilienOverviews.Commands.CreateOverview;
using BE.Application.ImmobilienOverviews.Commands.DeleteOverview;
using BE.Application.ImmobilienOverviews.Commands.GetAllOverview;
using BE.Application.ImmobilienOverviews.Commands.GetOverviewById;
using BE.Application.ImmobilienOverviews.Commands.UpdateOverviewById;
using BE.Application.ImmobilienOverviews.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class ImmobilienOverviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImmobilienOverviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImmobilienOverview(CreateImmobilienOverviewCommand command)
        {
            int id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImmobilienOverviewDto>>> GetAll()
        {
            var overviews = await _mediator.Send(new GetAllImmobilienOverviewCommand());
            return Ok(overviews);
        }

        [HttpGet("{overviewId}")]
        public async Task<ActionResult<ImmobilienOverviewDto>> GetById([FromRoute] int overviewId)
        {
            var result = await _mediator.Send(new GetImmobilienOverviewByIdCommand(overviewId));
            return Ok(result);
        }

        [HttpPatch("{overviewId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int overviewId, UpdateImmobilienOverviewCommand command)
        {
            command.Id = overviewId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{overviewId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int overviewId)
        {
            await _mediator.Send(new DeleteImmobilienOverviewByIdCommand(overviewId));
            return NoContent();
        }
    }
}
