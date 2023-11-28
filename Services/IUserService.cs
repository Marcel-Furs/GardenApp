using GardenApp.API.Data.Models;
using System.Security.Claims;

namespace GardenApp.API.Services
{
    public interface IUserService
    {
        public interface IUserService
        {
            Task<User> Authorize(string username, string password);
            Task CreateUser(User user, string password);
            Task<User> GetUserFromRequest(ClaimsPrincipal claimsPrincipal);
        }
    }
}
