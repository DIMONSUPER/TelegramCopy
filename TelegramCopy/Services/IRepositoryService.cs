using System.Linq.Expressions;
using TelegramCopy.Models;

namespace TelegramCopy.Services;

public interface IRepositoryService
{
    Task<List<T>> GetAllAsync<T>()
        where T : class, IDTO, new();

    Task<T> GetSingleByIdAsync<T>(int id)
        where T : class, IDTO, new();

    Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class, IDTO, new();

    Task<List<T>> FindWhereAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class, IDTO, new();

    Task DeleteAsync<T>(T entity)
        where T : class, IDTO, new();

    Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class, IDTO, new();

    Task DeleteAllAsync<T>()
        where T : class, IDTO, new();

    Task DeleteAllAsync<T>(IEnumerable<T> entities)
        where T : class, IDTO, new();

    Task<int> SaveOrUpdateAsync<T>(T entity)
        where T : class, IDTO, new();

    Task SaveOrUpdateRangeAsync<T>(IEnumerable<T> entities)
        where T : class, IDTO, new();

    Task<int> CountAsync<T>()
        where T : class, IDTO, new();

    Task<int> SaveAsync<T>(T entity)
        where T : class, IDTO, new();
}

