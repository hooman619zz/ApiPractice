
namespace FirstApiProject.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task Update(T entity);
    }
}