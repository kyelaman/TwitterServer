using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitterServer.Models.Entities;
using TwitterServer.Requests.User;
using TwitterServer.Responses.User;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetUserRequest getUserRequest)
        {
            var user = await _userService.GetUserByEmailAddressAsync(getUserRequest.EmailAddress);

            if (user == null)
            {
                var getUserResponse = new GetUserResponse
                {
                    Error = new Models.Error
                    {
                        Code = "123",
                        Message = $"There is no user by {getUserRequest.EmailAddress}"
                    }
                };

                return BadRequest(getUserResponse);
            }
            else
            {
                var getUserResponse = new GetUserResponse
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    EmailAddress = user.EmailAddress
                };

                return Ok(getUserResponse);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                Username = createUserRequest.Username,
                EmailAddress = createUserRequest.EmailAddress,
            };

            var userId = await _userService.CreateUserAsync(user);
            
            var createUserResponse = new CreateUserResponse
            {
                UserId = userId
            };

            return new ObjectResult(createUserResponse)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}
