using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence.Models;

[Table("programari", Schema = "ANTRENORI")]
public partial class programari
{
    public int id_antrenor { get; set; }

    [Key]
    public int id { get; set; }

    public int id_client { get; set; }

    public DateOnly date { get; set; }

    [Column(TypeName = "char")]
    public char? status { get; set; }
}
