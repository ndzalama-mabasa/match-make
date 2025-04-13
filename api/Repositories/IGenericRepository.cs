namespace galaxy_match_make.Repositories;

using System.Collections.Generic;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
}