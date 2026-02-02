public class UserValidator : IUserValidator
{
    public bool ValidatePassword(string password)
    {
        return password.Length >= 8; // мінімальна довжина пароля
    }
}