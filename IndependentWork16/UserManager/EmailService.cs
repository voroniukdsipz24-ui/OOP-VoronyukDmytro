public class EmailService : IEmailService
{
    public void SendWelcomeEmail(string email)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine($"Вітальний лист відправлено на {email}");
    }
}