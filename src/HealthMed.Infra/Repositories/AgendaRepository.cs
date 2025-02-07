using HealthMed.Application.Contracts;
using HealthMed.Domain.Context;
using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repositories;

public class AgendaRepository : IAgendaRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private DbSet<Agenda> _dbSet;

    public AgendaRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Agendas;
    }

    public Task<Agenda> Get(Guid id)
    {
        return _dbSet.FirstAsync(a => a.Id == id);
    }

    public Task<List<Agenda>> Get(Guid medicoId, DateTime? data = null)
    {
        var query = _dbSet.Where(a => a.MedicoId == medicoId).AsQueryable();

        if (data != null)
        {
            query = query.Where(a => a.Data == data);
        }

        return query.ToListAsync();
    }

    public Task Create(Agenda agenda)
    {
        _dbSet.AddAsync(agenda);
        return _applicationDbContext.SaveChangesAsync();
    }

    public Task Update(Agenda agenda)
    {
        _dbSet.Update(agenda);
        return _applicationDbContext.SaveChangesAsync();
    }
}