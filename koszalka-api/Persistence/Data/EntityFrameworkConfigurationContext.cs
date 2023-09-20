using koszalka_api.Persistence.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace koszalka_api.Persistence.Data
{
    public class EntityFrameworkConfigurationContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public EntityFrameworkConfigurationContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Shoes> Shoes { get; set; }

    }

}
