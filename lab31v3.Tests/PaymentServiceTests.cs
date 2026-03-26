using Moq;
using Xunit;

public class PaymentServiceTests
{
    [Fact]
    public void MakePayment_ShouldReturnTrue_WhenGatewaySucceeds()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        gatewayMock.Setup(g => g.ProcessPayment(100)).Returns(true);

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        var result = service.MakePayment(100);

        Assert.True(result);
    }

    [Fact]
    public void MakePayment_ShouldReturnFalse_WhenGatewayFails()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        gatewayMock.Setup(g => g.ProcessPayment(100)).Returns(false);

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        var result = service.MakePayment(100);

        Assert.False(result);
    }

    [Fact]
    public void MakePayment_ShouldLogSuccess_WhenPaymentSuccessful()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        gatewayMock.Setup(g => g.ProcessPayment(It.IsAny<decimal>())).Returns(true);

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        service.MakePayment(50);

        loggerMock.Verify(l => l.Log("Payment successful"), Times.Once);
    }

    [Fact]
    public void MakePayment_ShouldLogFailure_WhenPaymentFails()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        gatewayMock.Setup(g => g.ProcessPayment(It.IsAny<decimal>())).Returns(false);

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        service.MakePayment(50);

        loggerMock.Verify(l => l.Log("Payment failed"), Times.Once);
    }

    [Fact]
    public void MakePayment_ShouldReturnFalse_WhenAmountIsZero()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        var result = service.MakePayment(0);

        Assert.False(result);
    }

    [Fact]
    public void MakePayment_ShouldReturnFalse_WhenAmountNegative()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        var result = service.MakePayment(-10);

        Assert.False(result);
    }

    [Fact]
    public void MakePayment_ShouldLogInvalidAmount()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        service.MakePayment(0);

        loggerMock.Verify(l => l.Log("Invalid payment amount"), Times.Once);
    }

    [Fact]
    public void MakePayment_ShouldCallGateway()
    {
        var gatewayMock = new Mock<IPaymentGateway>();
        var loggerMock = new Mock<ITransactionLogger>();

        gatewayMock.Setup(g => g.ProcessPayment(It.IsAny<decimal>())).Returns(true);

        var service = new PaymentService(gatewayMock.Object, loggerMock.Object);

        service.MakePayment(100);

        gatewayMock.Verify(g => g.ProcessPayment(100), Times.Once);
    }
}