public class PaymentService
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly ITransactionLogger _logger;

    public PaymentService(IPaymentGateway paymentGateway, ITransactionLogger logger)
    {
        _paymentGateway = paymentGateway;
        _logger = logger;
    }

    public bool MakePayment(decimal amount)
    {
        if (amount <= 0)
        {
            _logger.Log("Invalid payment amount");
            return false;
        }

        bool result = _paymentGateway.ProcessPayment(amount);

        if (result)
        {
            _logger.Log("Payment successful");
        }
        else
        {
            _logger.Log("Payment failed");
        }

        return result;
    }
}