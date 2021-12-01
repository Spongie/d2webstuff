using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Database
{
    public class UserDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbName = "auth.db";
                var dbEnvironment = Environment.GetEnvironmentVariable("AUTH_DB_PATH");

                if (!string.IsNullOrEmpty(dbEnvironment))
                {
                    dbName = dbEnvironment;
                }

                optionsBuilder.UseSqlite($"DataSource={dbName};");
            }
        }
    }
}
