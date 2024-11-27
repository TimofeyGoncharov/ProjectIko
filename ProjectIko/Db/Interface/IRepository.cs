namespace ProjectIko.Db.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<List<T>> GetAllReadOnlyAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(long id);

        Task InsertAsync(T entity);

        Task<T> InsertAndReturnAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAsync(Guid id);

        Task DeleteAsync(int id);

        Task SaveChanges();

        Task ReloadContext(T entity);
    }
}
