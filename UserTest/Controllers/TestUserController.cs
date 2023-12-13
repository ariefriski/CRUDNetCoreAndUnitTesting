using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCrud;
using UserCrud.Controllers;
using UserTest.MockData;

namespace UserTest.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAllAsync()).ReturnsAsync(UserMockData.GetUsers());
            var sut = new UserController(userService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetAllAsync();


            // /// Assert
            result.StatusCode.Should().Be(200);//Should().Be(200);//.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn204NoContentStatus()
        {
            /// Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAllAsync()).ReturnsAsync(UserMockData.GetEmptyUsers());
            var sut = new UserController(userService.Object);

            /// Act
            var result = (NoContentResult)await sut.GetAllAsync();


            /// Assert
            result.StatusCode.Should().Be(204);
            userService.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task SaveAsync_ShouldCall_IUserService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var userService = new Mock<IUserService>();
            var newTodo = UserMockData.NewUser();
            var sut = new UserController(userService.Object);

            /// Act
            var result = await sut.SaveAsync(newTodo);

            /// Assert
            userService.Verify(_ => _.SaveAsync(newTodo), Times.Exactly(1));
        }
    }
}
