using Microsoft.EntityFrameworkCore;
using WAMS.Backend.Constants;
using WAMS.Backend.Model;
using WAMS.Components.Model;

namespace WAMS.Backend.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions options) : base(options) {}

      public DbSet<Class> Classes { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<UserAccountPolicy> Policies { get; set; }
   }
}
