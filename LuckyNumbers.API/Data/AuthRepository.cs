using System;
using System.Text;
using System.Threading.Tasks;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;

        public AuthRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> login(string username, string password)
        {
            var user = await context.users.FirstOrDefaultAsync(x => x.username == username);

            if (user == null)
            {
                return null;
            }

            if( !VerifyPasswordHash(password, user.passwordHash, user.passwordSalt) ) {
                return null;
            }

            return user;
        }

        public async Task<User> register(User user, string password)
        {
            UserRegisterDto userDto = new UserRegisterDto();
            byte[] passwordHash, passwordSalt;
            createPasswordHashSalt(password, out passwordHash, out passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            await context.users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }


        public async Task<bool> userExists(string username)
        {
            if (await context.users.AnyAsync(x => x.username == username))
            {
                return true;
            }

            return false;
        }

        private void createPasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using( var hmac = new System.Security.Cryptography.HMACSHA512() ) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using( var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt) ) {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if( computeHash[i] != passwordHash[i] ) {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}