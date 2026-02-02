class Program
{
    static void Main(string[] args)
    {
        // Створення залежностей
        IUserValidator userValidator = new UserValidator();
        IUserRepository userRepository = new UserRepository();
        IPasswordHasher passwordHasher = new PasswordHasher();
        IEmailService emailService = new EmailService();

        // Створення UserService
        UserService userService = new UserService(userValidator, userRepository, passwordHasher, emailService);

        // Реєстрація користувача
        userService.RegisterUser("ivan_ivanov", "strongPassword123", "ivanov@mail.com");
    }
}