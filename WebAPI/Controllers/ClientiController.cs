using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence.Models;
using WebAPI.Validators;
using WebAPI.Dtos;
using WebAPI.Mappers;
namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ClientiController : ControllerBase
{
    private readonly FitnessDbContext _db;
    public  ClientiController(FitnessDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await _db.Set<client>().AsNoTracking().ToListAsync(ct);
        return Ok(items);
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> Create(int id, [FromBody] ClientCreateDto dto, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest("Id is required.");

        // make sure the trainer exists
        var trainerExists = await _db.Set<Antrenori>()
                                     .AnyAsync(a => a.antrenor_id == id, ct);
        if (!trainerExists)
            return NotFound($"No Antrenor found with id {id}.");

        var trainerName = await _db.Set<Antrenori>()
            .Where( a=> a.antrenor_id == id).Select(a => (a.first_name ?? "") + " " + (a.last_name ?? ""))
            .Select( s => s.Trim() )
            .FirstOrDefaultAsync(ct);

        var errors = ClientValidator.ValidateForCreate(dto);
        if (errors.Count > 0)
            return ValidationProblem(new ValidationProblemDetails(errors));

        // map dto -> entity and set the FK from the route
        var entity = dto.ToEntity();
        entity.antrenor_id = id;

        _db.Set<client>().Add(entity);
        await _db.SaveChangesAsync(ct);

        return Created($"/api/clienti/{entity.id_client}", entity);
    }

    // DELETE /api/clienti/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var entity = await _db.Set<client>()
                              .FirstOrDefaultAsync(c => c.id_client == id, ct);

        if (entity is null)
            return NotFound();

        _db.Set<client>().Remove(entity);

        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            // e.g., FK/constraint issues
            return Conflict(new { message = "Delete failed due to constraints.", detail = ex.Message });
        }

        return Ok(new
        {
            deleted = new
            {
                entity.id_client,
                entity.first_name,
                entity.last_name,
                entity.username,
                entity.antrenor_id
            },
            deletedAt = DateTimeOffset.UtcNow
        });
    }
}

