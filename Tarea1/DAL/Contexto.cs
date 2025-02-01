using Microsoft.EntityFrameworkCore;
using Tarea1.Models;

namespace Tarea1.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }


    public DbSet<Tecnicos> Tecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Tickets> Tickets  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tickets>()
             .HasOne(t => t.Tecnico)
             .WithMany(t => t.Tickets)
             .OnDelete(DeleteBehavior.ClientCascade);
    }
}
