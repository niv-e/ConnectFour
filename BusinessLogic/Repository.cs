using BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic
{
    public class Repository<T> : DbContext, IRepository<T> where T : class
    {
        private DbSet<T> dbSet { get; set; }
        //public Repository(DbContextOptions<T> options)
        //    : base(options)
        //{}

        public Task Delete(T entity)
        {
            dbSet.Remove(entity);
            return Task.CompletedTask;
        }


        public Task Insert(T entity)
        {
            return dbSet.AddAsync(entity)
                .AsTask();

        }

        public Task Update(T entity)
        {
                dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task<T?> GetById<V>(V id) where V : struct
        {
            return dbSet.FindAsync(id)
                .AsTask();
        }
    }

}
