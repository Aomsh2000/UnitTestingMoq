using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingUsingMoq.Controllers;
using UnitTestingUsingMoq.Models;
using UnitTestingUsingMoq.Repositories;

namespace UnitTestsForUnitTestingUsingMoq
{
   
        [TestFixture]
        public class OrderControllerTests
        {
            private Mock<IOrderRepository> _mockRepo;
            private OrderController _controller;

            [SetUp]
            public void Setup()
            {
                _mockRepo = new Mock<IOrderRepository>();
                _controller = new OrderController(_mockRepo.Object);
            }

            [Test]
            public async Task Create_ValidOrder_ReturnsOk()
            {
                var order = new Order { OrderId = 1, UserId = 1, Product = "Laptop", Quantity = 2, Price = 1500 };
                await _controller.Create(order);
                _mockRepo.Verify(r => r.AddOrder(order), Times.Once);
            }

            [Test]
            public async Task Get_ExistingId_ReturnsOrder()
            {
                var order = new Order { OrderId = 1, Product = "Laptop" };
                _mockRepo.Setup(r => r.GetByOrderId(1)).ReturnsAsync(order);

                var result = await _controller.Get(1) as OkObjectResult;

                Assert.That(result, Is.Not.Null);
                Assert.That((result.Value as Order)?.Product, Is.EqualTo("Laptop"));
            }

            [Test]
            public async Task Get_NonExistingId_ReturnsNotFound()
            {
                _mockRepo.Setup(r => r.GetByOrderId(It.IsAny<int>())).ReturnsAsync((Order)null);
                var result = await _controller.Get(999);
                Assert.IsInstanceOf<NotFoundResult>(result);
            }

            [Test]
            public async Task Update_ValidOrder_CallsUpdate()
            {
                var order = new Order { OrderId = 1, Product = "Updated Product" };
                await _controller.Update(order);
                _mockRepo.Verify(r => r.UpdateOrder(order), Times.Once);
            }

            [Test]
            public async Task Delete_ValidId_CallsDelete()
            {
                await _controller.Delete(1);
                _mockRepo.Verify(r => r.DeleteOrder(1), Times.Once);
            }
        }
    
}
