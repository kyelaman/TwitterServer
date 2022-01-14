using TwitterServer.Models;
using TwitterServer.Models.Entities;

namespace TwitterServer.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(User user);

        Task<User> GetUserByEmailAddressAsync(string emailAddress);
    }
}
