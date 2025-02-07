using HealthMed.Application.Contracts;
using HealthMed.Domain.Context;
using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private DbSet<Consulta> _dbSet;

    public ConsultaRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Consultas;
    }

    public async Task Create(Consulta consulta)
    {
        await _dbSet.AddAsync(consulta);
        await _applicationDbContext.SaveChangesAsync();
    }

    public Task<List<Consulta>> Get(Guid medicoId, DateTime? data = null)
    {
        var query = _dbSet.Where(c => c.MedicoId == medicoId).AsQueryable();
        if (data.HasValue)
        {
            query = query.Where(c => c.Data.Date == data.Value.Date);
        }
        
        return query.ToListAsync();
    }

    public Task<bool> ValidaSeExisteConsulta(Guid medicoId, DateTime data, TimeSpan horario)
    {
        return _dbSet.AnyAsync(c => c.Horario == horario && c.MedicoId == medicoId && c.Data.Date == data.Date);
    }
}