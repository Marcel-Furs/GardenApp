using GardenApp.API.Data.Models;

namespace GardenApp.API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user, string username);
    }
}
