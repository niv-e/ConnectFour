using BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic
{
    public static class RepositoryFactory 
    {

        public static IRepository<T> CreateRepository<T>(IConfiguration configuration) where T : class
        { 

            var optionsBuilder = new DbContextOptionsBuilder<Repository<T>>();
            
            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new Repository<T>(optionsBuilder.Options);
        }
    }
}
