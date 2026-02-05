public class PayPalService : IPaymentGateway
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing PayPal payment of {amount}...");
    }
}
