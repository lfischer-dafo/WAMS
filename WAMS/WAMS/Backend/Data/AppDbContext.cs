using Microsoft.EntityFrameworkCore;
using WAMS.Components.Model;

namespace WAMS.Backend.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions options) : base(options) {}

      public DbSet<Class> Classes { get; set; }
      public DbSet<User> Users { get; set; }
   }
}
