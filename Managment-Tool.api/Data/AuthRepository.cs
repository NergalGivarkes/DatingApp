using System;
using System.Threading.Tasks;
using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;
namespace DatingApp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _contect;
        public AuthRepository(DataContext contect)
        {
            _contect = contect;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _contect.Users.FirstOrDefaultAsync(x => x.Username == username);
            if(user == null)
            {
                return null;
            }

            if(!VerfyPasswordhash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerfyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
              using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                    for(int i = 0; i < computedHash.Length; i++)
                    {
                        if(computedHash[i] != passwordHash[i])
                        {
                            return false;
                        }
                    }
                }

                return true;
        
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _contect.Users.AddAsync(user);
            await _contect.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
                using(var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _contect.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            }

            return false;
        }
    }
}