namespace GardenApp.API.Services
{
    public interface ITokenService
    {
        string CreateToken(string userId, string username);
    }
}
