namespace WebAPI.Dtos;

public sealed class AntrenorUpdateDto
{
    public string? tip { get; set; }
    public string? tip_abonament { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? nr_telefon { get; set; }
    public string? mail { get; set; }
    public string? username { get; set; }
    public string? password { get; set; } // allowed to update, but not returned
}
