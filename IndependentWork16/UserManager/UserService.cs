public class UserService
{
    private readonly IUserValidator _userValidator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;

    public UserService(IUserValidator userValidator, IUserRepository userRepository,
        IPasswordHasher passwordHasher, IEmailService emailService)
    {
        _userValidator = userValidator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
    }

    public void RegisterUser(string username, string password, string email)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (!_userValidator.ValidatePassword(password))
        {
            Console.WriteLine("Пароль не є дійсним!");
            return;
        }

        string hashedPassword = _passwordHasher.HashPassword(password);
        _userRepository.SaveUser(username, hashedPassword, email);
        _emailService.SendWelcomeEmail(email);

        Console.WriteLine("Користувач успішно зареєстрований!");
    }
}