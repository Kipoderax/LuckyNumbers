using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LuckyNumbers.API.Entities;
using Newtonsoft.Json;

namespace LuckyNumbers.API.Data
{
    public class Seed
    {
        private readonly DataContext context;
        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void seedUsers() {

            if(!context.users.Any()) {
                var userData = File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach(var user in users) {
                    byte[] passwordHash, passwordSalt;
                    createPasswordHashSalt("password", out passwordHash, out passwordSalt);

                    user.passwordHash = passwordHash;
                    user.passwordSalt = passwordSalt;
                    user.username = user.username.ToLower();

                    context.users.Add(user);
                }

                context.SaveChanges();
            }

            
        }

        private void createPasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using( var hmac = new System.Security.Cryptography.HMACSHA512() ) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}