using BE.Application.Bruttomietrenditen.Commands.CreateBruttomietrendite;
using BE.Application.Bruttomietrenditen.Commands.UpdateBruttomietrendite;
using BE.Application.ImmobilienHausgelder.Commands.CreateHausgeld;
using BE.Application.ImmobilienHausgelder.Commands.UpdateHausgeld;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview")]
    public class BruttomietrenditeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BruttomietrenditeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public async Task<IActionResult> Create([FromRoute] int overviewId, [FromBody] CreateBruttomietrenditeCommand command)
        //{
        //    command.ImmobilienOverviewId = overviewId;
        //    int bruttomietrenditeId = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetById), new { bruttomietrenditeId }, null);
        //}

        [HttpPatch("bruttomietrendite/{bruttomietrenditeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int bruttomietrenditeId, [FromBody] UpdateBruttomietrenditeByIdCommand command)
        {
            command.Id = bruttomietrenditeId;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
