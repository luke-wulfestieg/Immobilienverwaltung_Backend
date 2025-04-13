using AutoMapper;
using Immobilienverwaltung_Backend.Data;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.DTOs;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class Immobilien_HausgeldController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Immobilien_HausgeldController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("byImmobilienOverviewId/{immobilienOverviewId}")]
    public async Task<ActionResult<Immobilien_Hausgeld_DTO>> GetHausgeldByImmobilienOverviewId(int immobilienOverviewId)
    {
        var hausgeldEntity = await _context.ImmobilienHausgelder
                                           .FirstOrDefaultAsync(h => h.ImmobilienOverviewId == immobilienOverviewId);

        if (hausgeldEntity == null)
        {
            return NotFound();
        }

        var hausgeldDto = _mapper.Map<Immobilien_Hausgeld_DTO>(hausgeldEntity);
        return Ok(hausgeldDto);
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<Immobilien_Hausgeld_DTO>>> GetHausgelder()
    {
        var entities = await _context.ImmobilienHausgelder.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<Immobilien_Hausgeld_DTO>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Immobilien_Hausgeld_DTO>> GetHausgeldById(int id)
    {
        var hausgeldEntity = await _context.ImmobilienHausgelder
                                            .FirstOrDefaultAsync(h => h.Id == id);

        if (hausgeldEntity == null)
        {
            return NotFound();
        }

        var hausgeldDto = _mapper.Map<Immobilien_Hausgeld_DTO>(hausgeldEntity);
        return Ok(hausgeldDto);
    }

    [HttpPost]
    public async Task<ActionResult<Immobilien_Hausgeld_DTO>> CreateHausgeld(Immobilien_Hausgeld_DTO dto)
    {
        var entity = _mapper.Map<Immobilien_Hausgeld>(dto);
        _context.ImmobilienHausgelder.Add(entity);
        await _context.SaveChangesAsync();

        var createdDto = _mapper.Map<Immobilien_Hausgeld_DTO>(entity);
        return CreatedAtAction(nameof(GetHausgeldById), new { id = entity.Id }, createdDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Immobilien_Hausgeld_DTO>> UpdateHausgeld(int id, Immobilien_Hausgeld_DTO dto)
    {
        var entity = await _context.ImmobilienHausgelder.FindAsync(id);
        if (entity == null) return NotFound();

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<Immobilien_Hausgeld_DTO>(entity));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHausgeld(int id)
    {
        var entity = await _context.ImmobilienHausgelder.FindAsync(id);
        if (entity == null) return NotFound();

        // Optional: Prevent delete if linked to an ImmobilienOverview
        var isUsed = await _context.ImmobilienOverviews.AnyAsync(io => io.ImmobilienHausgeldId == id);
        if (isUsed)
        {
            return BadRequest("This Hausgeld is associated with a property and cannot be deleted directly.");
        }

        _context.ImmobilienHausgelder.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
