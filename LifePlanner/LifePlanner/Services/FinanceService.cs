using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LifePlanner.Data;
using LifePlanner.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LifePlanner.Services
{
    public class FinanceService
    {
        private IDbContextFactory<MainDBContext> _dbContextFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FinanceService(IDbContextFactory<MainDBContext> dbContextFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public void AddFinanceItem(FinanceData fItem)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Finances.Add(fItem);
                context.SaveChanges();
            }
        }

        public IEnumerable<FinanceData> GetAllFinanceDataForUser()
        {
            var userId = GetLoggedInUserId();

            // If the user is not logged in or if user ID is not found, return empty list
            if (!userId.HasValue)
                return Enumerable.Empty<FinanceData>();

            using (var context = _dbContextFactory.CreateDbContext())
            {
                // Filter finance data based on user ID
                return context.Finances.Where(f => f.UserId == userId.Value).ToList();
            }
        }

        public IEnumerable<FinanceData> GetFinanceDataType(string type)
        {
            var userId = GetLoggedInUserId();

            // If the user is not logged in or if user ID is not found, return empty list
            if (!userId.HasValue)
                return Enumerable.Empty<FinanceData>();

            using (var context = _dbContextFactory.CreateDbContext())
            {
                // Filter finance data based on user ID
                return context.Finances.Where(f => f.UserId == userId.Value && f.Type == type).ToList();
            }
        }


        /*
        public IEnumerable<FinanceData> GetAllFinanceData()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Finances
             .Where(finance => context.Users.Any(user => user.Id == finance.UserId))
            .ToList();

            }
        }
        */

        private int? GetLoggedInUserId()
        {
            // Get the user's ID from claims
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue("UserId");
            Console.WriteLine(userIdClaim);
            // Parse the user ID if it exists
            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                return userId;

            return null;
        }
    }
}
