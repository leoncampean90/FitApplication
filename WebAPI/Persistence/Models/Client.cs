using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence.Models;

[Table("client", Schema = "CLIENT")]
public partial class Clienti
{
    [Key]
    public int id_client { get; set; }

    [Column(TypeName = "char")]
    public char username { get; set; }

    [Column(TypeName = "char")]
    public char password { get; set; }

    public int antrenor_id { get; set; }

    [Column(TypeName = "char")]
    public char first_name { get; set; }

    [Column(TypeName = "char")]
    public char last_name { get; set; }

    [Column(TypeName = "char")]
    public char nr_telefon { get; set; }

    [Column(TypeName = "char")]
    public char? mail { get; set; }

    [Column(TypeName = "char")]
    public char tip { get; set; }

    public DateOnly? date { get; set; }

    public int? nr_sesiuni { get; set; }

    public bool plata_lunara { get; set; }

    [Column(TypeName = "char")]
    public char? dimensiune_brat { get; set; }

    [Column(TypeName = "char")]
    public char? dimensiune_piept { get; set; }

    [Column(TypeName = "char")]
    public char? dimensiune_burta { get; set; }

    [Column(TypeName = "char")]
    public char? dimensiune_picior { get; set; }
}
