using Microsoft.EntityFrameworkCore;
using PlataformaEncuestas.Models;

namespace PlataformaEncuestas.Data;

public class EncuestasContext : DbContext
{
    public EncuestasContext(DbContextOptions<EncuestasContext> options)
        : base(options)
    {
    }

    public DbSet<EncuestaModel> Encuestas { get; set; }
    public DbSet<OpcionModel> Opciones { get; set; }
    public DbSet<VotoModel> Votos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EncuestaModel>()
            .HasMany(e => e.Opciones)
            .WithOne(o => o.Encuesta)
            .HasForeignKey(o => o.EncuestaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EncuestaModel>()
            .HasMany(e => e.Votos)
            .WithOne(v => v.Encuesta)
            .HasForeignKey(v => v.EncuestaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OpcionModel>()
            .HasMany(o => o.Votos)
            .WithOne(v => v.Opcion)
            .HasForeignKey(v => v.OpcionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VotoModel>()
            .HasIndex(v => new { v.EncuestaId, v.IdentificadorVotante })
            .IsUnique();
    }
}