using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tarea1.DAL;
using Tarea1.Models;

namespace Tarea1.Services;

public class TicketsService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Tickets tickets)
    {
        if (!await Existe(tickets.TicketId))
        {
            return await Insertar(tickets);
        }
        else
        {
            return await Modificar(tickets);
        }
    }

    private async Task<bool> Existe(int TicketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets 
            .AnyAsync(t => t.TicketId == TicketId);
    }

    private async Task<bool> Insertar(Tickets tickets)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(tickets);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tickets tickets)
    {
        try
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(tickets);
            return await contexto
                .SaveChangesAsync() > 0;
        }
        catch(Exception e)
        {
            return false;
        }
    }

    public async Task<Tickets?> Buscar(int TicketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.FirstOrDefaultAsync(t => t.TicketId == TicketId);
    }

    public async Task<bool> Eliminar(int TicketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .AsNoTracking()
            .Where(t => t.TicketId == TicketId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets 
            .Where(criterio)
            .Include(t => t.cliente)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> TicketsExist(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Where(t => t.TicketId == ticketId)
            .AnyAsync();
    }
}
