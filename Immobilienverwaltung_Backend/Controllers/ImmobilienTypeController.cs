using BE.Application.ImmobilienTypes.Commands.CreateTypes;
using BE.Application.ImmobilienTypes.Commands.DeleteTypes;
using BE.Application.ImmobilienTypes.Commands.GetAllTypes;
using BE.Application.ImmobilienTypes.Commands.GetTypesById;
using BE.Application.ImmobilienTypes.Commands.UpdateTypes;
using BE.Application.ImmobilienTypes.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Immobilienverwaltung_Backend.Controllers
{
    [ApiController]
    [Route("api/overview/immobilien-type")]
    public class ImmobilienTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImmobilienTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImmobilienType([FromBody] CreateImmobilienTypeCommand command)
        {
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImmobilienTypeDto>>> GetAll()
        {
            var types = await _mediator.Send(new GetAllImmobilienTypesCommand());
            return Ok(types);
        }

        [HttpGet("{typeId}")]
        public async Task<ActionResult<ImmobilienTypeDto>> GetById([FromRoute] int typeId)
        {
            var result = await _mediator.Send(new GetImmobilienTypesByIdCommand(typeId));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPatch("{typeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int typeId, [FromBody] UpdateImmobilienTypeCommand command)
        {
            command.Id = typeId;

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{typeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int typeId)
        {
            await _mediator.Send(new DeleteImmobilienTypesByIdCommand(typeId));
            return NoContent();
        }
    }
}
