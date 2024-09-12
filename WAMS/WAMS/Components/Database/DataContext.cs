
using Microsoft.EntityFrameworkCore;
using WAMS.Components.Model;

namespace WAMS.Components.Database
{
    public class DataContext : DbContext
    {
        const string connectionString = "Server=45.85.219.82; User ID=berufsschule; Password=wY?{!.:5<2\"F12P)/{KYbH.q4Vl5Doq:I296W4£iHN%y8s`L,.; Database=MAMS";

        public DbSet<Class> Classes { get; set; }

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
