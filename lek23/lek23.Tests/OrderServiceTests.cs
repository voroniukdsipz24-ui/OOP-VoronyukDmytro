using Xunit;
using Moq;
using lek23.Interfaces;
using lek23.Models;
using lek23.Services;

namespace lek23.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepo;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _mockRepo = new Mock<IOrderRepository>();
            _service = new OrderService(_mockRepo.Object);
        }

        [Fact]
        public void CalculateDiscount_VipCustomer_Returns15Percent()
        {
            var order = new Order { Amount = 100, IsVipCustomer = true };

            var result = _service.CalculateDiscount(order);

            Assert.Equal(15, result);
        }

        [Theory]
        [InlineData(100, false, 5)]
        [InlineData(200, false, 10)]
        public void CalculateDiscount_RegularCustomer_Returns5Percent(
            decimal amount, bool vip, decimal expected)
        {
            var order = new Order { Amount = amount, IsVipCustomer = vip };

            var result = _service.CalculateDiscount(order);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDiscount_InvalidAmount_ThrowsException()
        {
            var order = new Order { Amount = 0 };

            Assert.Throws<System.ArgumentException>(
                () => _service.CalculateDiscount(order));
        }

        [Fact]
        public void CreateOrder_ValidOrder_CallsRepositorySaveOnce()
        {
            var order = new Order { Id = 1, Amount = 100 };

            _service.CreateOrder(order);

            _mockRepo.Verify(r => r.Save(order), Times.Once);
        }

        [Fact]
        public void GetOrder_ValidId_ReturnsOrder()
        {
            _mockRepo.Setup(r => r.GetById(1))
                     .Returns(new Order { Id = 1, Amount = 100 });

            var result = _service.GetOrder(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetOrder_InvalidId_ThrowsException()
        {
            Assert.Throws<System.ArgumentException>(
                () => _service.GetOrder(0));
        }
    }
}
