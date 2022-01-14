using Microsoft.EntityFrameworkCore;
using TwitterServer.Models;
using TwitterServer.Models.Entities;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.Services.Implementations
{
    public class UserService : IUserService
    {
        public async Task<Guid> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedOn = DateTime.UtcNow;

            using (var db = new TwitterContext())
            {
                await db.Users.AddAsync(user);

                await db.SaveChangesAsync();

                return user.Id;
            }
        }

        public async Task<User> GetUserByEmailAddressAsync(string emailAddress)
        {
            using (var db = new TwitterContext())
            {
                var users = await db.Users.Where(x => x.EmailAddress == emailAddress).ToListAsync();

                return users.SingleOrDefault();
            }
        }
    }
}
