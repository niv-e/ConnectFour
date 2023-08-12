using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourWinformClient.Model
{
    public class ClientDbContext : DbContext
    {
        public DbSet<SessionRecordEntity> SessionRecord { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var runtimeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string output = runtimeFolder.Substring(0, runtimeFolder.IndexOf(@"\bin"));
            optionsBuilder.UseSqlServer(@$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={output}\ClientDb.mdf;Integrated Security=True");
        }

    }
}
