namespace GardenApp.API.Services
{
    public interface IHashService
    {
        void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool CompareHash(string passwordStr, byte[] passwordHash, byte[] passwordSalt);
    }
}
