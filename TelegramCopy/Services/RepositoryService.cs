using System.Linq.Expressions;
using SQLite;
using TelegramCopy.Models;

namespace TelegramCopy.Services;

public class RepositoryService : IRepositoryService
{
    private readonly Lazy<SQLiteAsyncConnection> _lazySQLiteConnection;
    private readonly string _lazySQLiteConnectionPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME);

    public RepositoryService()
    {
        _lazySQLiteConnection = CreateLazySQLiteConnection();
    }

    #region -- IRepositoryService implementation --

    public async Task DeleteAllAsync<T>()
            where T : class, IDTO, new()
    {
        await _lazySQLiteConnection.Value.DropTableAsync<T>();
        await _lazySQLiteConnection.Value.CreateTableAsync<T>();
    }

    public Task<int> CountAsync<T>()
        where T : class, IDTO, new()
    {
        return _lazySQLiteConnection.Value.Table<T>().CountAsync();
    }

    public Task<List<T>> GetAllAsync<T>()
        where T : class, IDTO, new()
    {
        return _lazySQLiteConnection.Value.Table<T>().ToListAsync();
    }

    public Task<T> GetSingleByIdAsync<T>(int id)
        where T : class, IDTO, new()
    {
        return GetSingleAsync<T>(x => x.Id == id);
    }

    public async Task<int> SaveAsync<T>(T entity)
        where T : class, IDTO, new()
    {
        var row = -1;

        if (entity is not null)
        {
            row = await _lazySQLiteConnection.Value.InsertAsync(entity);
        }

        return row;
    }

    public async Task<int> SaveOrUpdateAsync<T>(T entity)
        where T : class, IDTO, new()
    {
        var row = -1;

        if (entity is not null)
        {
            var existEntity = await GetSingleByIdAsync<T>(entity.Id);

            if (existEntity is null)
            {
                row = await _lazySQLiteConnection.Value.InsertAsync(entity);
            }
            else
            {
                entity.Id = existEntity.Id;
                row = await _lazySQLiteConnection.Value.UpdateAsync(entity);
            }
        }

        return row;
    }

    public async Task DeleteAsync<T>(T entity)
        where T : class, IDTO, new()
    {
        if (entity is not null)
        {
            await _lazySQLiteConnection.Value.DeleteAsync(entity);
        }
    }

    public async Task SaveOrUpdateRangeAsync<T>(IEnumerable<T> entities)
        where T : class, IDTO, new()
    {
        if (entities is not null && entities.Any())
        {
            foreach (var entity in entities)
            {
                await SaveOrUpdateAsync(entity);
            }
        }
    }

    public async Task DeleteAllAsync<T>(IEnumerable<T> entities)
        where T : class, IDTO, new()
    {
        if (entities is not null && entities.Any())
        {
            foreach (var entity in entities)
            {
                await DeleteAsync(entity);
            }
        }
    }

    public async Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class, IDTO, new()
    {
        var allMatched = await FindWhereAsync(predicate);

        return allMatched?.FirstOrDefault();
    }

    public Task<List<T>> FindWhereAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class, IDTO, new()
    {
        return _lazySQLiteConnection.Value.Table<T>().Where(predicate).ToListAsync();
    }

    public async Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate)
        where T : class, IDTO, new()
    {
        var result = await FindWhereAsync(predicate);

        await DeleteAllAsync(result);
    }

    #endregion

    #region -- Private helpers --

    private Lazy<SQLiteAsyncConnection> CreateLazySQLiteConnection()
    {
        return new(() =>
        {
            var database = new SQLiteAsyncConnection(_lazySQLiteConnectionPath);

            database.CreateTableAsync<Message>().Wait();
            database.CreateTableAsync<ChatInfo>().Wait();

            return database;
        });
    }

    #endregion
}

