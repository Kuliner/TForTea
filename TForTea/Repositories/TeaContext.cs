namespace TForTea.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TForTea.Models;

    public class TeaContext : DbContext
    {
        private bool created;

        public TeaContext()
        {
            if (!this.created)
            {
                this.created = true;
                this.Database.EnsureCreated();
            }
        }

        public DbSet<Tea> Teas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=tea.db");
        }
    }
}