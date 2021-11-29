using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySystem.Application.Interface.Managers;
using CompanySystem.Application.Models;
using CompanySystem.Application.Views;
using CompanySystem.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CompanySystem.Tests.ControllerTests
{
    public class UserTests
    {
        private readonly Mock<IUserManager> _manager;
        private readonly UserController _userController;

        public UserTests()
        {
            _manager = new Mock<IUserManager>();
            _userController = new UserController(_manager.Object);
        }

        #region Positive Scenarios

        [Fact]
        public async Task POST_AddNewUser_Success()
        {
            // Arrange
            var model = GetUserModel();

            var view = GetUserViews().First();

            _manager.Setup(x => x.CreateUser(It.IsAny<UserModel>()))
                .Returns(Task.FromResult(view));

            // Act
            var response = await _userController.CreateUser(model);

            var result = (OkObjectResult) response;
            var resultedView = (UserView) result.Value;

            // Assert
            _manager.Verify(x => x.CreateUser(It.IsAny<UserModel>()), Times.Once);
            Assert.Equal(view.Id, resultedView.Id);
            Assert.Equal(view.FirstName, resultedView.FirstName);
            Assert.Equal(view.LastName, resultedView.LastName);
            Assert.Equal(view.Email, resultedView.Email);
            Assert.Equal(view.Password, resultedView.Password);
            Assert.Equal(view.CompanyName, resultedView.CompanyName);
        }

        [Fact]
        public async Task PUT_UpdateUser_Success()
        {
            // Arrange
            var model = GetUserModel();

            var view = GetUserViews().First();

            _manager.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<UserModel>()))
                .Returns(Task.FromResult(view));

            // Act
            var response = await _userController.UpdateUser(1, model);

            var result = (OkObjectResult) response;
            var resultedView = (UserView) result.Value;

            // Assert
            _manager.Verify(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<UserModel>()), Times.Once);
            Assert.Equal( view.Id, resultedView.Id);
            Assert.Equal(view.FirstName, resultedView.FirstName);
            Assert.Equal(view.LastName, resultedView.LastName);
            Assert.Equal(view.Email, resultedView.Email);
            Assert.Equal(view.Password, resultedView.Password);
            Assert.Equal(view.CompanyName, resultedView.CompanyName);
        }

        [Fact]
        public async Task DELETE_DeleteUser_Success()
        {
            // Arrange
            _manager.Setup(x => x.DeleteUser(It.IsAny<int>()));

            // Act
            var response = await _userController.DeleteUser(1);
            var result = (NoContentResult) response;

            // Assert
            Assert.NotNull(result);
            _manager.Verify(x => x.DeleteUser(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GET_GetAllUsers_Success()
        {
            // Arrange
            var users = GetUserViews();

            _manager.Setup(x => x.GetAllUsers())
                .Returns(Task.FromResult(users));

            // Act
            var response = await _userController.GetAllUsers();
            var result = (OkObjectResult) response;
            var resultedView = (List<UserView>)result.Value;

            // Assert
            Assert.NotNull(result);
            _manager.Verify(x => x.GetAllUsers(), Times.Once);
            Assert.Equal(users.Count, resultedView.Count);
        }

        [Fact]
        public async Task GET_GetUserById_Success()
        {
            // Arrange
            var user = GetUserViews().First();

            _manager.Setup(x => x.GetUserById(It.IsAny<int>()))
                .Returns(Task.FromResult(user));

            // Act
            var response = await _userController.GetUserById(user.Id);
            var result = (OkObjectResult)response;
            var resultedView = (UserView)result.Value;

            // Assert
            Assert.NotNull(result);
            _manager.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            Assert.Equal(user.Id, resultedView.Id);
            Assert.Equal(user.FirstName, resultedView.FirstName);
            Assert.Equal(user.LastName, resultedView.LastName);
            Assert.Equal(user.Email, resultedView.Email);
            Assert.Equal(user.Password, resultedView.Password);
            Assert.Equal(user.CompanyName, resultedView.CompanyName);
        }

        #endregion

        #region Negative Scenarios (Some of them)

        [Fact]
        public async Task POST_AddNewUser_With_InvalidModel_Fail()
        {
            // Act
            var response = await _userController.CreateUser(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task PUT_UpdateUser_With_InvalidId_Fail()
        {
            // Act
            var response = await _userController.UpdateUser(-1, GetUserModel());

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Get_GetUser_With_InvalidId_Fail()
        {
            // Act
            var response = await _userController.GetUserById(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task DELETE_DeleteUser_With_InvalidId_Fail()
        {
            // Act
            var response = await _userController.DeleteUser(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }


        #endregion

        #region Helpers

        private UserModel GetUserModel()
        {
            return new()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@test.com",
                Password = "password"
            };
        }

        private List<UserView> GetUserViews()
        {
            return new()
            {
                new()
                {
                    Id = 1,
                    FirstName = "firstName-01",
                    LastName = "lastName-01",
                    Email = "email-01@test.com",
                    Password = "password-01",
                    CompanyName = null
                },
                new()
                {
                    Id = 2,
                    FirstName = "firstName-02",
                    LastName = "lastName-02",
                    Email = "email-02@test.com",
                    Password = "password-02",
                    CompanyName = "Company-01"
                }
            };
        }

        #endregion
    }
}