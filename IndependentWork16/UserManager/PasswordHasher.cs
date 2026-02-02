public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        // Імітація хешування пароля
        return $"hashed_{password}";
    }
}