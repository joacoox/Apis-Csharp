using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Models;

namespace SuperHeroApi.Repository
{
    public class DataContext : DbContext      
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {   
        }

        public DbSet<SuperHero> superheroes {  get; set; }
    }
}
