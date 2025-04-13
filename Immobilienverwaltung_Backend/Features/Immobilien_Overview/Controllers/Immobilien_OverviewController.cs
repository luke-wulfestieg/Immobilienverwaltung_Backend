using AutoMapper;
using Immobilienverwaltung_Backend.Data;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.DTOs;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models;
using Immobilienverwaltung_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class Immobilien_OverviewController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Immobilien_OverviewController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Immobilien_Overview_DTO>>> GetImmobilien()
    {
        var entities = await _context.ImmobilienOverviews
            .Include(i => i.ImmobilienType)
            .Include(i => i.ImmobilienHausgeld)
            .ToListAsync();

        var dtos = _mapper.Map<IEnumerable<Immobilien_Overview_DTO>>(entities);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Immobilien_Overview_DTO>> GetImmobilienById(int id)
    {
        var entity = await _context.ImmobilienOverviews
            .Include(i => i.ImmobilienType)
            .Include(i => i.ImmobilienHausgeld)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (entity == null)
            return NotFound();

        var dto = _mapper.Map<Immobilien_Overview_DTO>(entity);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<Immobilien_Overview_DTO>> CreateImmobilien(Immobilien_Overview_DTO dto)
    {
        // Validate ImmobilienType
        var immobilienType = await _context.ImmobilienTypes
            .FirstOrDefaultAsync(i => i.Id == dto.ImmobilienType.Id);

        if (immobilienType == null)
            return BadRequest("Invalid ImmobilienTypeId.");

        // First, map and create the ImmobilienOverview WITHOUT Hausgeld
        var entity = _mapper.Map<ImmobilienOverview>(dto);
        entity.ImmobilienType = immobilienType;
        entity.ImmobilienHausgeld = null; // We'll assign it later

        _context.ImmobilienOverviews.Add(entity);
        await _context.SaveChangesAsync(); // entity.Id will now be available

        // Create Hausgeld based on Wohnflaeche
        var proMonat = (decimal)(3 * dto.Wohnflaeche);
        var proJahr = proMonat * 12;

        var hausgeldEntity = new Immobilien_Hausgeld
        {
            Hausgeld = new QuadratmeterMonatJahr
            {
                proQuadratmeter = 3,
                proMonat = proMonat,
                proJahr = proJahr
            },
            Umlagefaehiges_Hausgeld = new ProzentMonatJahr
            {
                inProzent = 60,
                proMonat = proMonat * 0.6m,
                proJahr = (proMonat * 0.6m) * 12
            },
            Nicht_Umlagefaehiges_Hausgeld = new ProzentMonatJahr
            {
                inProzent = 40,
                proMonat = proMonat * 0.4m,
                proJahr = (proMonat * 0.4m) * 12
            },
            ImmobilienOverviewId = entity.Id // ✅ Correctly use the generated ID
        };

        _context.ImmobilienHausgelder.Add(hausgeldEntity);
        await _context.SaveChangesAsync();

        // Link Hausgeld to the Overview
        entity.ImmobilienHausgeld = hausgeldEntity;
        await _context.SaveChangesAsync();

        var createdDto = _mapper.Map<Immobilien_Overview_DTO>(entity);
        return CreatedAtAction(nameof(GetImmobilienById), new { id = entity.Id }, createdDto);
    }



    [HttpPut("{id}")]
    public async Task<ActionResult<Immobilien_Overview_DTO>> UpdateImmobilien(int id, Immobilien_Overview_DTO dto)
    {
        var entity = await _context.ImmobilienOverviews
            .Include(i => i.ImmobilienType)
            .Include(i => i.ImmobilienHausgeld)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (entity == null)
            return NotFound();

        // Update primitive fields directly
        entity.ImmobilienName = dto.ImmobilienName;
        entity.Kaufpreis = (uint)dto.Kaufpreis;
        entity.Wohnflaeche = dto.Wohnflaeche;
        entity.ImmobilienUeberschuss = dto.ImmobilienUeberschuss;
        entity.BruttoMietRendite = dto.BruttoMietRendite;
        entity.ZimmerAnzahl = dto.ZimmerAnzahl;


        // ... update other primitive properties here

        // Update ImmobilienType (just assign the FK if only using existing types)
        entity.ImmobilienTypeId = dto.ImmobilienType.Id;

        // Update Hausgeld fields directly on the tracked entity
        if (entity.ImmobilienHausgeld != null && dto.ImmobilienHausgeld != null)
        {
            var hausgeld = entity.ImmobilienHausgeld;

            var wohnflaeche = Convert.ToDecimal(dto.Wohnflaeche);

            hausgeld.Hausgeld.proQuadratmeter = dto.ImmobilienHausgeld.Hausgeld.proQuadratmeter;
            hausgeld.Hausgeld.proMonat = hausgeld.Hausgeld.proQuadratmeter * wohnflaeche;
            hausgeld.Hausgeld.proJahr = hausgeld.Hausgeld.proMonat * 12;

            hausgeld.Umlagefaehiges_Hausgeld.inProzent = dto.ImmobilienHausgeld.Umlagefaehiges_Hausgeld.inProzent;
            hausgeld.Umlagefaehiges_Hausgeld.proMonat = (hausgeld.Umlagefaehiges_Hausgeld.inProzent / 100) * hausgeld.Hausgeld.proMonat;
            hausgeld.Umlagefaehiges_Hausgeld.proJahr = hausgeld.Umlagefaehiges_Hausgeld.proMonat * 12;

            hausgeld.Nicht_Umlagefaehiges_Hausgeld.inProzent = dto.ImmobilienHausgeld.Nicht_Umlagefaehiges_Hausgeld.inProzent;
            hausgeld.Nicht_Umlagefaehiges_Hausgeld.proMonat = (hausgeld.Nicht_Umlagefaehiges_Hausgeld.inProzent / 100) * hausgeld.Hausgeld.proMonat;
            hausgeld.Nicht_Umlagefaehiges_Hausgeld.proJahr = hausgeld.Nicht_Umlagefaehiges_Hausgeld.proMonat * 12;
        }

        await _context.SaveChangesAsync();

        var result = _mapper.Map<Immobilien_Overview_DTO>(entity);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImmobilien(int id)
    {
        var entity = await _context.ImmobilienOverviews
            .Include(i => i.ImmobilienType)
            .Include(i => i.ImmobilienHausgeld)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (entity == null)
            return NotFound();

        _context.ImmobilienOverviews.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
