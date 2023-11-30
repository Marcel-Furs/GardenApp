using GardenApp.API.Data.Models;
using GardenApp.API.Data.Repositories;
using GardenApp.API.Exceptions;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GardenApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User, int> userRepository;
        private readonly IHashService hashService;

        public UserService(IBaseRepository<User, int> userRepository, IHashService hashService)
        {
            this.userRepository = userRepository;
            this.hashService = hashService;
        }

        public async Task<int> AuthUser(string username, string password)
        {
            var user = await userRepository.Get(x => x.Username == username);
            if (user == null || !hashService.CompareHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new InvalidCredentialsException("Invalid credentials");
            }
            return user.Id;
        }

        public void RegisterUser(string username, string password)
        {
            var user = userRepository.Find(x => x.Username == username);
            if (user != null)
            {
                throw new InvalidCredentialsException($"User with name {username} already exists!");
            }

            hashService.CreateHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            userRepository.Add(new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
        }
    }
}