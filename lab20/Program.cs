class Program
{
    static void Main()
    {
        IOrderValidator validator = new SimpleOrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        OrderService orderService = new OrderService(
            validator,
            repository,
            emailService
        );

        Console.WriteLine("=== VALID ORDER ===");
        Order validOrder = new Order(1, "John Doe", 150);
        orderService.ProcessOrder(validOrder);

        Console.WriteLine("\n=== INVALID ORDER ===");
        Order invalidOrder = new Order(2, "Jane Doe", -10);
        orderService.ProcessOrder(invalidOrder);
    }
}

