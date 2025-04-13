using System;
using UnitTestingUsingMoq.Repositories;
using UnitTestingUsingMoq.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using UnitTestingUsingMoq.Models;
using Microsoft.AspNetCore.Mvc;
using UnitTestingUsingMoq.Controllers;
namespace UnitTestsForUnitTestingUsingMoq
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockRepo;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IUserRepository>();
            _controller = new UserController(_mockRepo.Object);
        }

        [Test]
        public async Task Create_ValidUser_ReturnsOk()
        {
            var user = new User { Id = 1, FirstName = "Ali", LastName="Salih",Email="Ali@gmail.com" };
            await _controller.Create(user);
            _mockRepo.Verify(r => r.AddUser(user), Times.Once);
        }

        [Test]
        public async Task Get_ExistingId_ReturnsUser()
        {
            var user = new User { Id = 1, FirstName = "Ali" };
            _mockRepo.Setup(r => r.GetByUserId(1)).ReturnsAsync(user);

            var result = await _controller.Get(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That((result.Value as User)?.FirstName, Is.EqualTo("Ali"));
        }

        [Test]
        public async Task Get_NonExistingId_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetByUserId(It.IsAny<int>())).ReturnsAsync((User)null);
            var result = await _controller.Get(999);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Update_ValidUser_CallsUpdate()
        {
            var user = new User { Id = 1, FirstName = "Updated" };
            await _controller.Update(user);
            _mockRepo.Verify(r => r.UpdateUser(user), Times.Once);
        }

        [Test]
        public async Task Delete_ValidId_CallsDelete()
        {
            await _controller.Delete(1);
            _mockRepo.Verify(r => r.DeleteUser(1), Times.Once);
        }
    }
}