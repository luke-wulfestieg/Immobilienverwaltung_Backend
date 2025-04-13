using AutoMapper;
using Immobilienverwaltung_Backend.Data;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.DTOs;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class Immobilien_TypeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Immobilien_TypeController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Immobilien_Type_DTO>>> GetTypes()
    {
        var entities = await _context.ImmobilienTypes.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<Immobilien_Type_DTO>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Immobilien_Type_DTO>> GetById(int id)
    {
        var entity = await _context.ImmobilienTypes.FindAsync(id);
        if (entity == null) return NotFound();

        return Ok(_mapper.Map<Immobilien_Type_DTO>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<Immobilien_Type_DTO>> Create(Immobilien_Type_DTO dto)
    {
        var entity = _mapper.Map<Immobilien_Type>(dto);
        _context.ImmobilienTypes.Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<Immobilien_Type_DTO>(entity));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Immobilien_Type_DTO>> Update(int id, Immobilien_Type_DTO dto)
    {
        var entity = await _context.ImmobilienTypes.FindAsync(id);
        if (entity == null) return NotFound();

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<Immobilien_Type_DTO>(entity));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.ImmobilienTypes.FindAsync(id);
        if (entity == null) return NotFound();

        var isUsed = await _context.ImmobilienOverviews.AnyAsync(io => io.ImmobilienTypeId == id);
        if (isUsed)
        {
            return BadRequest("This ImmobilienType is assigned to a property and cannot be deleted.");
        }

        _context.ImmobilienTypes.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
