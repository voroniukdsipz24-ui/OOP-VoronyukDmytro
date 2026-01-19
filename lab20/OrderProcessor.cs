public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine("Validating order...");
        if (order.TotalAmount <= 0)
        {
            Console.WriteLine("Order is invalid!");
            return;
        }

        Console.WriteLine("Saving order to database...");
        Console.WriteLine($"Order {order.Id} saved.");

        Console.WriteLine("Sending email to customer...");
        Console.WriteLine($"Email sent to {order.CustomerName}");

        order.Status = OrderStatus.Processed;
        Console.WriteLine("Order processed successfully.");
    }
}
