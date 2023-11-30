using GardenApp.API.Data.Models;
using System.Security.Claims;

namespace GardenApp.API.Services
{
    public interface IUserService
    {
        void RegisterUser(string username, string password);
        Task<int> AuthUser(string username, string password);
    }
}