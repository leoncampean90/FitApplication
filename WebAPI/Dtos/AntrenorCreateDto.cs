namespace WebAPI.Dtos;

// DTO for create (no ID from FE)
public sealed class AntrenorCreateDto
{
    public string? tip { get; set; }                // optional; default to "a" in controller
    public string tip_abonament { get; set; } = default!;
    public string first_name { get; set; } = default!;
    public string last_name { get; set; } = default!;
    public string nr_telefon { get; set; } = default!;
    public string? mail { get; set; }
    public string username { get; set; } = default!;
    public string password { get; set; } = default!;
}