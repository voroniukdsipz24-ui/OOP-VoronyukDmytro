using System;

class UserManager
{
    public void RegisterUser(string username, string password)
    {
        SaveToDatabase(username, password);
        SendEmail(username);
        WriteLog(username);
    }

    private void SaveToDatabase(string username, string password)
    {
        Console.WriteLine("Saving user to database");
    }

    private void SendEmail(string username)
    {
        Console.WriteLine("Sending confirmation email");
    }

    private void WriteLog(string username)
    {
        Console.WriteLine($"User {username} registered");
    }
}


class UserService
{
    private readonly UserRepository _repository;
    private readonly EmailService _emailService;
    private readonly Logger _logger;

    public UserService(
        UserRepository repository,
        EmailService emailService,
        Logger logger)
    {
        _repository = repository;
        _emailService = emailService;
        _logger = logger;
    }

    public void RegisterUser(string username, string password)
    {
        _repository.Save(username, password);
        _emailService.SendConfirmation(username);
        _logger.Log("User registered");
    }
}

class UserRepository
{
    public void Save(string username, string password)
    {
        Console.WriteLine("Saving user to database");
    }
}

class EmailService
{
    public void SendConfirmation(string username)
    {
        Console.WriteLine("Sending confirmation email");
    }
}

class Logger
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}
