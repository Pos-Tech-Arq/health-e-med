using HealthMed.Domain.Context;
using HealthMed.Domain.Contracts;
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
}