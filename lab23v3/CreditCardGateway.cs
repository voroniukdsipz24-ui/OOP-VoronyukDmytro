public class CreditCardGateway : IPaymentGateway
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing credit card payment of {amount}...");
    }
}
