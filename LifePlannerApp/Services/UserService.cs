using LifePlannerApp.Data;
using LifePlannerApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LifePlannerApp.Services
{
    public class UserService
    {
        private IDbContextFactory<MainDBContext> _dbContextFactory;

        public UserService(IDbContextFactory<MainDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void AddUser(User user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void GetUserByName(string name)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users.SingleOrDefault(x => x.FirstName == name);
            }
        }
    }
}
