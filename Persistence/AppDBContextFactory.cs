using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebApi")).AddJsonFile("appsettings.json").Build();

            var connectionString = config.GetConnectionString("A2SV_Contest_Central");
            var builder = new DbContextOptionsBuilder<AppDBContext>();
            builder.UseNpgsql(connectionString);
            return new AppDBContext(builder.Options);
        }
    }
}
