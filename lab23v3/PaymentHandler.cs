public class PaymentHandler
{
    private readonly IPaymentGateway _paymentGateway;

    public PaymentHandler(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public void Pay(double amount)
    {
        _paymentGateway.ProcessPayment(amount);
    }
}
