namespace BusinessLogic
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
    }
}