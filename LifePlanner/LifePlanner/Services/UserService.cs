using LifePlanner.Data;
using LifePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LifePlanner.Services
{
    public class UserService
    {
        private IDbContextFactory<MainDBContext> _dbContextFactory;

        public UserService(IDbContextFactory<MainDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void AddUser (User user)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Users.ToList();
            }
        }

        public User GetUserByName (string firstName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users.SingleOrDefault(x => x.FirstName == firstName);
                return user;
            }
        }

        public void UpdateUser (string firstName, string password)
        {
            var user = GetUserByName (firstName);
            if (user == null) 
            {
                throw new Exception("Customer does not exist");
            }
            user.Password = password;
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Update(user);
                context.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users.Find(userId);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
