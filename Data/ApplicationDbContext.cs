using Microsoft.EntityFrameworkCore;
using ComponentProcessing.API.Models;

namespace ComponentProcessing.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<ComponentProcessingModel> componentProcessings { get; set; }
    }
}
