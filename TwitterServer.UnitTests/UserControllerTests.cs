using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TwitterServer.Controllers;
using TwitterServer.Models.Entities;
using TwitterServer.Requests.User;
using TwitterServer.Responses.User;
using TwitterServer.Services.Interfaces;

namespace TwitterServer.UnitTests
{
    public class UserControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_AutoMock_UserService()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange   

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "TestFName",
                    LastName = "TestLName",
                    EmailAddress = "test@Test.com"
                };

                var createUserRequest = new CreateUserRequest
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress
                };

                mock.Mock<IUserService>().Setup(x => x.CreateUserAsync(It.IsAny<User>())).Returns(Task.FromResult(user.Id));

                var userController = mock.Create<UserController>();

                //Act
                var result = await userController.Post(createUserRequest);

                //Assert
                mock.Mock<IUserService>().Verify(x => x.CreateUserAsync(It.IsAny<User>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf<ObjectResult>(result);
                var objectResult = (ObjectResult)result;
                Assert.NotNull(objectResult.Value);
                Assert.IsInstanceOf<CreateUserResponse>(objectResult.Value);
                var createUserResponse = (CreateUserResponse)objectResult.Value;
                Assert.AreEqual(user.Id, createUserResponse.UserId);
            }
        }
    }
}