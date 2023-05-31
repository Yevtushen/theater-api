using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    public interface IUserService
    {
        string GetLoginData(string username, string password);
        Task<bool> Login(string username, string password);
        Task SignUp(string username, string password);
    }
    public class UserService : IUserService, IDisposable
    {
        private readonly ApplicationContext _dbContext;
        private readonly SHA256 _sha;

        public UserService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _sha = SHA256.Create();
        }

        public void Dispose()
        {
            _sha.Dispose();
        }

        public string GetLoginData(string username, string password) //Task<bool>
        {
            var tmp = $"{username}:{password}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(tmp));
            /* var passcode = HashPassword(password);
             return await _dbContext.Users.AnyAsync(x => x.Password == passcode && x.Email.ToLower() == username.ToLower());*/
        }

        public async Task<bool> Login(string username, string password)
        {
            var passcode = HashPassword(password);
            return await _dbContext.Users.AnyAsync(x => x.Password == passcode && x.Email.ToLower() == username.ToLower()); 
        }

        public async Task SignUp(string username, string password)
        {
            if (await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == username.ToLower()))
            {
                throw new InvalidOperationException();
            }
            var passcode = HashPassword(password);
            User user = new User()
            {
                Email = username.ToLower(),
                Password = passcode,
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            var hash = _sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
