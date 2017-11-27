using Microsoft.EntityFrameworkCore;
using recipeconfigurationservice.Model;

namespace recipeconfigurationservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        //Extract
        public DbSet<Extract> Extracts{get;set;}
        public DbSet<ExtractConfiguration> ExtractConfigurations{get;set;}
        public DbSet<ExtractInParameter> ExtractInParameters{get;set;}
        public DbSet<ExtractOutParameter> ExtractOutParameters{get;set;}
        public DbSet<ApiConfiguration> ApiConfiguration{get;set;}
        public DbSet<SqlConfiguration> SqlConfiguration{get;set;}
        //Load
        public DbSet<Load> Loads{get;set;}
        public DbSet<LoadConfiguration> LoadConfigurations {get;set;}
        public DbSet<SqlLoad> SqlLoads{get;set;}
        public DbSet<ApiLoad> Apiloads{get;set;}
        public DbSet<ParameterLoad> ParameterLoads{get;set;}



    }
}