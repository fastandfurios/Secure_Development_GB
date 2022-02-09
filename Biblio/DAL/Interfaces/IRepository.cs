namespace Biblio.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken token=default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token=default);
        Task<T> GetEntityAsync(string id, CancellationToken token=default);
        Task UpdateAsync(T entity, string id, CancellationToken token=default);
        Task DeleteAsync(string id, CancellationToken token=default);
    }
}
