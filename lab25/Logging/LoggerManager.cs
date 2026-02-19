using System;

namespace lab25.Logging;

public class LoggerManager
{
    private static LoggerManager? _instance;
    private static readonly object _lock = new();

    private LoggerFactory? _factory;

    private LoggerManager() { }

    public static LoggerManager Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new LoggerManager();
                return _instance;
            }
        }
    }

    public void SetFactory(LoggerFactory factory)
    {
        _factory = factory;
    }

    public ILogger GetLogger()
    {
        if (_factory == null)
            throw new InvalidOperationException("Фабрику логера не встановлено.");

        return _factory.CreateLogger();
    }
}
