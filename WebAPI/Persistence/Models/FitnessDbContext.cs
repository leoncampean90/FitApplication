using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Persistence.Models;

public partial class FitnessDbContext : DbContext
{
    public FitnessDbContext()
    {
    }

    public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Antrenori> antrenoris { get; set; }

    public virtual DbSet<client> clients { get; set; }

    public virtual DbSet<programari> programaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FitnessApp;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Antrenori>(entity =>
        {
            entity.HasKey(e => e.antrenor_id).HasName("antrenori_pkey");

            entity.Property(e => e.antrenor_id).UseIdentityAlwaysColumn();
            entity.Property(e => e.tip).HasDefaultValueSql("'a'::\"char\"");
        });

        modelBuilder.Entity<client>(entity =>
        {
            entity.HasKey(e => e.id_client).HasName("client_pkey");

            entity.Property(e => e.id_client).UseIdentityAlwaysColumn();
            entity.Property(e => e.plata_lunara).HasDefaultValue(false);
            entity.Property(e => e.tip).HasDefaultValueSql("'c'::character varying");

            entity.HasOne(d => d.antrenor).WithMany(p => p.clients)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("client_antrenor_fk");
        });

        modelBuilder.Entity<programari>(entity =>
        {
            entity.HasKey(e => e.id).HasName("programari_pkey");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
