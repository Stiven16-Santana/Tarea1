using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tarea1.DAL;
using Tarea1.Models;

namespace Tarea1.Services
{
    public class SistemasService
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public SistemasService(IDbContextFactory<Contexto> dbFactory)
        {
            DbFactory = dbFactory;
        }

        public async Task<bool> Guardar(Sistemas sistema)
        {
            if (sistema.SistemaId == 0 || !await Existe(sistema.SistemaId))
            {
                return await Insertar(sistema); // Si no existe, inserta
            }
            else
            {
                return await Modificar(sistema); // Si existe, modifica
            }
        }

        private async Task<bool> Existe(int sistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas
                .AnyAsync(s => s.SistemaId == sistemaId);
        }

        private async Task<bool> Insertar(Sistemas sistema)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Sistemas.Add(sistema);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Sistemas sistema)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Sistemas.Update(sistema);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Sistemas?> Buscar(int sistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas.FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
        }

        public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int sistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var sistema = await contexto.Sistemas.FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
            if (sistema != null)
            {
                contexto.Sistemas.Remove(sistema);
                return await contexto.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
