using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace koszalka_api.Data
{
    public class EntityFrameworkContext : IEntityFrameworkContext
    {
        private readonly IConfiguration _iConfiguration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EntityFrameworkConfigurationContext>(options => {
                options.UseSqlServer(_iConfiguration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
