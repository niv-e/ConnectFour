namespace BusinessLogic.Contracts
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T?> GetById<V>(V id) where V : struct;
    }
}