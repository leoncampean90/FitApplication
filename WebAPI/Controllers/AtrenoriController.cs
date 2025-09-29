using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence.Models;
using WebAPI.Dtos;
using WebAPI.Validators;
using WebAPI.Mappers;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntrenoriController : ControllerBase
{
    private readonly FitnessDbContext _db;
    public AntrenoriController(FitnessDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await _db.Set<Antrenori>()
            .AsNoTracking()
            .Select(a => new
            {
                a.antrenor_id,
                a.tip,
                a.tip_abonament,
                a.first_name,
                a.last_name,
                a.nr_telefon,
                a.mail,
                a.username,
                clients = a.clients.Select(c => new
                {
                    c.id_client,
                    c.first_name,
                    c.last_name,
                    c.username,
                    c.antrenor_id
                }).ToList()
            })
            .ToListAsync(ct);

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var item = await _db.Set<Antrenori>()
            .AsNoTracking()
            .Where(a => a.antrenor_id == id)
            .Select(a => new
            {
                a.antrenor_id,
                a.tip,
                a.tip_abonament,
                a.first_name,
                a.last_name,
                a.nr_telefon,
                a.mail,
                a.username,
                clients = a.clients.Select(c => new
                {
                    c.id_client,
                    c.first_name,
                    c.last_name,
                    c.username,
                    c.antrenor_id
                }).ToList()
            })
            .FirstOrDefaultAsync(ct);

        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AntrenorCreateDto dto, CancellationToken ct)
    {
        var errors = AntrenorValidator.ValidateForCreate(dto);
        if (errors.Count > 0)
            return ValidationProblem(new ValidationProblemDetails(errors));

        var entity = dto.ToEntity();
        _db.Set<Antrenori>().Add(entity);
        await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = entity.antrenor_id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AntrenorUpdateDto dto, CancellationToken ct)
    {
        var errors = AntrenorValidator.ValidateForUpdate(dto);
        if (errors.Count > 0)
            return ValidationProblem(new ValidationProblemDetails(errors));

        var entity = await _db.Set<Antrenori>().FirstOrDefaultAsync(a => a.antrenor_id == id, ct);
        if (entity is null) return NotFound();

        var changed = entity.ApplyUpdate(dto);
        if (!changed)
            return BadRequest("No changes detected. The provided values are identical to the existing ones.");

        await _db.SaveChangesAsync(ct);

        var updated = new
        {
            entity.antrenor_id,
            entity.tip,
            entity.tip_abonament,
            entity.first_name,
            entity.last_name,
            entity.nr_telefon,
            entity.mail,
            entity.username
        };

        return Ok(new { updated, updatedAt = DateTimeOffset.UtcNow });
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var entity = await _db.Set<Antrenori>().FirstOrDefaultAsync(a => a.antrenor_id == id, ct);
        if (entity is null) return NotFound();

        _db.Set<Antrenori>().Remove(entity);
        await _db.SaveChangesAsync(ct);

        return Ok(new
        {
            deleted = new
            {
                entity.antrenor_id,
                entity.tip,
                entity.tip_abonament,
                entity.first_name,
                entity.last_name,
                entity.nr_telefon,
                entity.mail,
                entity.username
            },
            deletedAt = DateTimeOffset.UtcNow
        });
    }
}
