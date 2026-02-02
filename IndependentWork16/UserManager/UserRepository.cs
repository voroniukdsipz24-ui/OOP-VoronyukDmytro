public class UserRepository : IUserRepository
{
    public void SaveUser(string username, string password, string email)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Користувача збережено в базу даних.");
    }
}