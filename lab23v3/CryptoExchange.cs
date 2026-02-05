public class CryptoExchange : IPaymentGateway
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing cryptocurrency payment of {amount}...");
    }
}
