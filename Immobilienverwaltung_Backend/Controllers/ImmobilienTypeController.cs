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
    [Route("api/immobilien-type")]
    public class ImmobilienTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImmobilienTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all Immobilien Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImmobilienTypeDto>>> GetAll()
        {
            var types = await _mediator.Send(new GetAllImmobilienTypesCommand());
            return Ok(types);
        }

        // Get an Immobilien Type by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ImmobilienTypeDto>> GetById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetImmobilienTypesByIdCommand(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // Create a new Immobilien Type
        [HttpPost]
        public async Task<IActionResult> CreateImmobilienType([FromBody] CreateImmobilienTypeCommand command)
        {
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // Delete an Immobilien Type by ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            await _mediator.Send(new DeleteImmobilienTypesByIdCommand(id));
            return NoContent();
        }

        // Update an Immobilien Type by ID
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateImmobilienTypeCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
