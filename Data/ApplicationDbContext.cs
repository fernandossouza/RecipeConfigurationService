using Microsoft.EntityFrameworkCore;
using recipeconfigurationservice.Model;

namespace recipeconfigurationservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }


        public DbSet<Extract> Extracts{get;set;}
        public DbSet<ExtractConfiguration> ExtractConfigurations{get;set;}
        public DbSet<ExtractInParameter> ExtractInParameters{get;set;}
        public DbSet<ExtractOutParameter> ExtractOutParameters{get;set;}


    }
}