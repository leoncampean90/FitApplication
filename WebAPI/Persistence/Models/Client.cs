using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence.Models;

[Table("client", Schema = "CLIENT")]
[Index("mail", Name = "ux_client_mail", IsUnique = true)]
[Index("username", Name = "ux_client_username", IsUnique = true)]
public partial class client
{
    [Key]
    public int id_client { get; set; }

    [StringLength(50)]
    public string username { get; set; } = null!;

    [StringLength(100)]
    public string password { get; set; } = null!;

    public int antrenor_id { get; set; }

    [StringLength(100)]
    public string first_name { get; set; } = null!;

    [StringLength(100)]
    public string last_name { get; set; } = null!;

    [StringLength(20)]
    public string nr_telefon { get; set; } = null!;

    [StringLength(255)]
    public string? mail { get; set; }

    [StringLength(10)]
    public string tip { get; set; } = null!;

    public DateOnly? date { get; set; }

    public int? nr_sesiuni { get; set; }

    public bool plata_lunara { get; set; }

    [StringLength(100)]
    public string? dimensiune_brat { get; set; }

    [StringLength(100)]
    public string? dimensiune_piept { get; set; }

    [StringLength(100)]
    public string? dimensiune_burta { get; set; }

    [StringLength(100)]
    public string? dimensiune_picior { get; set; }

    [ForeignKey("antrenor_id")]
    [InverseProperty("clients")]
    public virtual Antrenori? antrenor { get; set; }
}
