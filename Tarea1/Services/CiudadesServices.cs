using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tarea1.DAL;
using Tarea1.Models;

namespace Tarea1.Services;

public class CiudadesServices
{
    private readonly IDbContextFactory<Contexto> DbFactory;
    public CiudadesServices(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Ciudades ciudad)
    {
        if (!await Existe(ciudad.CiudadId))
        {
            return await Insertar(ciudad);
        }
        else
        {
            return await Modificar(ciudad);
        }

    }

    private async Task<bool> Existe(int CiudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AnyAsync(c => c.CiudadId == CiudadId);

    }

    private async Task<bool> Insertar(Ciudades registrosCiudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Ciudades.Add(registrosCiudades);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Ciudades registrosCiudades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(registrosCiudades);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<Ciudades?> Buscar(int CiudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.FirstOrDefaultAsync(c => c.CiudadId == CiudadId);

    }

    public async Task<bool> Eliminar(int CiudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AsNoTracking()
            .Where(c => c.CiudadId == CiudadId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .Where(criterio)
            .Include(c => c.Ciudad)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> CiudadExist(string Ciudad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .Where(c => c.Ciudad == Ciudad)
            .AnyAsync();
    }
}
