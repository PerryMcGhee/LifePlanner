using LifePlanner.Data;
using LifePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
                user.Password = HashPassword(user.Password);
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
        public User GetUserByUsername(string username)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == username);
                return user;
            }
        }
        public User GetUserByEmail(string email)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.Users.SingleOrDefault(x => x.Email == email);
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
            user.Password = HashPassword(password);
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Update(user);
                context.SaveChanges();
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword);
            return hashOfEnteredPassword == storedHash;
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
