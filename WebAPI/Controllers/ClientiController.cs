using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence.Models;
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
        var items = await _db.Set<Clienti>().AsNoTracking().ToListAsync(ct);
        return Ok(items);
    }
}

