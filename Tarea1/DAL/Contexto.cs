using Tarea1.Models;
using Microsoft.EntityFrameworkCore;

namespace Tarea1.DAL
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }


        public DbSet<Tecnicos> Tecnicos { get; set; }

        public DbSet<Clientes> Clientes { get; set; }


    }
}
