using LifePlannerApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LifePlannerApp.Data
{
    public class MainDBContext : DbContext
    {

        public MainDBContext(DbContextOptions<MainDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<FinanceData> Finances { get; set; }
    }
}

