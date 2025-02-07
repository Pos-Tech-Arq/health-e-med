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

    public async Task Create(Agenda agenda)
    {
        await _dbSet.AddAsync(agenda);
        await _applicationDbContext.SaveChangesAsync();
    }

    public Task<Agenda> Get(Guid medicoId, DateTime data)
    {
        return _dbSet.FirstAsync(x => x.Id == medicoId && x.Data.Date == data.Date);
    }
}