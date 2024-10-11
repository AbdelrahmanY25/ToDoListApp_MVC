using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ToDoAppDbContext: DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true).Build();

            var connection = builder.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>()
                .Property(a => a.Name)
                .IsRequired(false);

            modelBuilder.Entity<Assignment>()
                .Property(p => p.PdfFile)
                .IsRequired(false);
        }
    }
}
