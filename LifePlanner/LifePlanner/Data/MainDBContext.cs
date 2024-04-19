using LifePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LifePlanner.Data
{
    public class MainDBContext :DbContext
    {

        public MainDBContext(DbContextOptions<MainDBContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
    }
}
