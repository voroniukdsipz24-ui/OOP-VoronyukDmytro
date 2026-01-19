public class ConsoleEmailService : IEmailService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"Sending email to {order.CustomerName}...");
    }
}
