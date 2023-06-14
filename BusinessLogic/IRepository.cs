namespace BusinessLogic
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
    }

    public class Repository<T> : IRepository<T> 
    {
        private IList<T> _entities = new List<T>();

        public Task Delete(T entity)
        {
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T> GetById(int id)
        {
            return await Task.FromResult(_entities.First());
        }

        public Task Insert(T entity)
        {
            _entities.Add(entity);
            return Task.CompletedTask;

        }

        public Task Update(T entity)
        {
            return Task.CompletedTask;
        }
    }

}