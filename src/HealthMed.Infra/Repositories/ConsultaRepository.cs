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
}