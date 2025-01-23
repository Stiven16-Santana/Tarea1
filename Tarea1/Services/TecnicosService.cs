using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Linq.Expressions;
using Tarea1.DAL;
using Tarea1.Models;

namespace Tarea1.Services
{
    public class TecnicosService (IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Tecnicos tecnico)
        {
            if(!await Existe(tecnico.TecnicoId))
             {
                return await Insertar(tecnico);

             }
            else
            {
                return await Modificar(tecnico);
            } 
        }
            
        private async Task<bool> Existe(int tecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AnyAsync(t => t.TecnicoId == tecnicoId);
          
        }

        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(tecnico);
            return await contexto
                .SaveChangesAsync() > 0;
        }

        public async Task<Tecnicos?> Buscar(int TecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);

        }

        public async Task<bool> Eliminar(int TecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AsNoTracking()
                .Where(t => t.TecnicoId == TecnicoId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> TecnicoNombreExist(string Nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .Where(t => t.Nombres == Nombre)
                .AnyAsync();
        }
    }
}
