using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Repositories;
using System.Linq.Expressions;

namespace NTierArchitecture.DataAccess.Repositories;
internal class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
       return await _dbContext.Set<T>().AnyAsync(expression, cancellationToken);
    }

    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>().AsNoTracking().AsQueryable();
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Where(expression).FirstOrDefaultAsync(cancellationToken);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
    {
        return _dbContext.Set<T>().AsNoTracking().Where(expression).AsQueryable();
    }

    public void Remove(T entity)
    {
        _dbContext.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }
}


