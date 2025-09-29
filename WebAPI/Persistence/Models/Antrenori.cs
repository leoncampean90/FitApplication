using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence.Models;

[Table("antrenori", Schema = "ANTRENORI")]
public partial class Antrenori
{
    [Key]
    public int antrenor_id { get; set; }

    [StringLength(10)]
    public string tip { get; set; } = null!;

    [StringLength(20)]
    public string tip_abonament { get; set; } = null!;

    [StringLength(100)]
    public string first_name { get; set; } = null!;

    [StringLength(100)]
    public string last_name { get; set; } = null!;

    [StringLength(20)]
    public string nr_telefon { get; set; } = null!;

    [StringLength(255)]
    public string? mail { get; set; }

    [StringLength(50)]
    public string username { get; set; } = null!;

    [StringLength(100)]
    public string password { get; set; } = null!;
}
