using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAMS.Backend.Model;
using WAMS.Components.Model;

namespace WAMS.Backend.Data
{
   public class ImportDbContext : DbContext
   {
      /// <value>
      /// Property <c>ConnectionString</c> contains the connection -and login information used to connect to the MySQL-Database.
      /// </value>
      public string ConnectionString = "server=127.0.0.1;Port=3306;database=WAMS;User=root;Password=QAYwsxedc;";

      public DbSet<Class> Classes { get; set; }
      public DbSet<Student> Users { get; set; }
      public DbSet<UserAccountPolicy> Policies { get; set; }

      /// <summary>
      /// Overridden function is called to connect to the database when an instance of <c>AppDbContext</c> is created.
      /// </summary>
      protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
   }

}
