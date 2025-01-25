using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tarea1.DAL;
using Tarea1.Models;

namespace Tarea1.Services
{
    public class ClientesService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Clientes cliente)
        {
            if (!await Existe(cliente.ClienteId))
            {
                return await Insertar(cliente);
            }
            else
            {
                return await Modificar(cliente);
            }

        }
             private async Task<bool> Existe(int ClienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AnyAsync(c => c.ClienteId ==ClienteId);

        }

        private async Task<bool> Insertar(Clientes cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Clientes cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(cliente);
            return await contexto
                .SaveChangesAsync() > 0;
        }

        public async Task<Clientes?> Buscar(int ClienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.FirstOrDefaultAsync(c => c.ClienteId == ClienteId);

        }

        public async Task<bool> Eliminar(int ClienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking()
                .Where(c => c.ClienteId == ClienteId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .Where(criterio)
                .Include(c => c.Tecnico)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ClienteNombreExist(string Nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .Where(c => c.Nombres == Nombre)
                .AnyAsync();
        }
        
        public async Task<bool> ClienteRNCExist(string RNC)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .Where(c => c.RNC == RNC)
                .AnyAsync();
        }       
    }

}