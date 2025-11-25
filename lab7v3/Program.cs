using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторна робота 7. Патерн retry та обробка io/мережевих помилок");
Console.WriteLine();

// 1. демонстрація fileprocessor
var fileProcessor = new FileProcessor();

try
{
    Console.WriteLine("=== Сценарій 1: читання рядків з файлу з тимчасовими помилками io ===");

    List<string> lines = RetryHelper.ExecuteWithRetry(
        operation: () => fileProcessor.GetLines("data/demo-file.txt"),
        retryCount: 3,
        initialDelay: TimeSpan.FromMilliseconds(500),
        shouldRetry: ex => ex is IOException
    );

    Console.WriteLine("Успішно прочитані рядки з файлу:");
    foreach (var line in lines)
    {
        Console.WriteLine($"  {line}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Кінцева помилка при роботі з файлом: {ex.GetType().Name} - {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Натисніть будь-яку клавішу для переходу до мережевого сценарію...");
Console.ReadKey();
Console.WriteLine();

// 2. демонстрація networkclient
var networkClient = new NetworkClient();

try
{
    Console.WriteLine("=== Сценарій 2: запит до api з тимчасовими мережевими помилками ===");

    string response = RetryHelper.ExecuteWithRetry(
        operation: () => networkClient.GetApiResponse("https://api.example.com/data"),
        retryCount: 5,
        initialDelay: TimeSpan.FromMilliseconds(500),
        shouldRetry: ex => ex is HttpRequestException
    );

    Console.WriteLine("Успішно отримана відповідь від api:");
    Console.WriteLine(response);
}
catch (Exception ex)
{
    Console.WriteLine($"Кінцева помилка при зверненні до api: {ex.GetType().Name} - {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Натисніть будь-яку клавішу для демонстрації вибіркового shouldRetry...");
Console.ReadKey();
Console.WriteLine();

// 3. демонстрація вибіркового shouldRetry
try
{
    Console.WriteLine("=== Сценарій 3: вибірковий shouldRetry (не повторювати для інших винятків) ===");

    string result = RetryHelper.ExecuteWithRetry<string>(
        operation: () =>
        {
            Console.WriteLine("Імітація операції з некоректним винятком");
            throw new InvalidOperationException("Ця помилка не повинна повторюватися");
        },
        retryCount: 3,
        initialDelay: TimeSpan.FromMilliseconds(300),
        shouldRetry: ex => ex is IOException || ex is HttpRequestException
    );

    Console.WriteLine($"Результат: {result}");
}
catch (Exception ex)
{
    Console.WriteLine("Операція не була повторена, оскільки тип винятку не підходить для shouldRetry");
    Console.WriteLine($"Кінцева помилка: {ex.GetType().Name} - {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Програма завершена. Натисніть будь-яку клавішу для виходу...");
Console.ReadKey();


// ======================= класи =======================

public class FileProcessor
{
    private int _attemptCounter = 0;

    public List<string> GetLines(string path)
    {
        _attemptCounter++;
        Console.WriteLine($"FileProcessor.GetLines. Виклик {_attemptCounter} для шляху \"{path}\"");

        if (_attemptCounter <= 2)
        {
            throw new IOException("Тимчасова помилка доступу до файлу");
        }

        return new List<string>
        {
            $"Рядок 1 з файлу {path}",
            "Рядок 2. Дані",
            "Рядок 3. Кінець файлу"
        };
    }
}

public class NetworkClient
{
    private int _attemptCounter = 0;

    public string GetApiResponse(string endpoint)
    {
        _attemptCounter++;
        Console.WriteLine($"NetworkClient.GetApiResponse. Виклик {_attemptCounter} для endpoint \"{endpoint}\"");

        if (_attemptCounter <= 4)
        {
            throw new HttpRequestException("Тимчасова мережева помилка");
        }

        return $"{{ \"status\": \"ok\", \"endpoint\": \"{endpoint}\", \"data\": [1, 2, 3] }}";
    }
}

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool>? shouldRetry = null)
    {
        if (operation == null)
            throw new ArgumentNullException(nameof(operation));

        if (retryCount < 1)
            throw new ArgumentOutOfRangeException(nameof(retryCount), "retryCount має бути не менше 1");

        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(500);

        Func<Exception, bool> retryPredicate = shouldRetry ?? (ex => true);

        int attempt = 0;

        while (true)
        {
            try
            {
                attempt++;
                Console.WriteLine();
                Console.WriteLine($"RetryHelper. Спроба {attempt}");

                T result = operation();

                Console.WriteLine($"RetryHelper. Успіх на спробі {attempt}");
                return result;
            }
            catch (Exception ex) when (attempt <= retryCount && retryPredicate(ex))
            {
                Console.WriteLine($"RetryHelper. Спроба {attempt} завершилась помилкою");
                Console.WriteLine($"  Тип: {ex.GetType().Name}");
                Console.WriteLine($"  Повідомлення: {ex.Message}");

                if (attempt == retryCount)
                {
                    Console.WriteLine("Досягнуто максимальну кількість спроб");
                    throw;
                }

                double multiplier = Math.Pow(2, attempt - 1);
                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * multiplier);

                Console.WriteLine($"Очікування {delay.TotalMilliseconds} мс перед наступною спробою");
                Thread.Sleep(delay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("RetryHelper. Виняток не підлягає повтору");
                Console.WriteLine($"  Тип: {ex.GetType().Name}");
                Console.WriteLine($"  Повідомлення: {ex.Message}");
                throw;
            }
        }
    }
}
