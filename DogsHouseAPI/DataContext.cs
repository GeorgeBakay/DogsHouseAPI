using DogsHouseAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
namespace DogsHouseAPI
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Dog> dogs { get; set; }
    }
}
