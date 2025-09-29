using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Dtos;


public sealed class ClientCreateDto {
    public int id_client { get; set; }
    public string username { get; set; } = default!;
    public string password { get; set; } = default!;
    public int antrenor_id { get; set; } = default!;
    public string first_name { get; set; } = default!;
    public string last_name { get; set; } = default!;
    public string nr_telefon { get; set; } = default!;
    public string? mail { get; set; }
    public string tip { get; set; } = default!;
    public DateOnly? date { get; set; }
    public int? nr_sesiuni { get; set; }
    public bool plata_lunara { get; set; } = default!;
    public string? dimensiune_brat { get; set; }
    public string? dimensiune_piept { get; set; }
    public string? dimensiune_burta { get; set; }
    public string? dimensiune_picior { get; set; }
    public string? antrenor { get; set; }
}