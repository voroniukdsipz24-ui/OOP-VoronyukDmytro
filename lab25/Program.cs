using System;
using lab25.Logging;
using lab25.Strategy;
using lab25.Observer;

namespace lab25;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("========== СЦЕНАРІЙ 1: ПОВНА ІНТЕГРАЦІЯ ==========");

        LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());

        var context = new DataContext(new EncryptDataStrategy());
        var publisher = new DataPublisher();
        var observer = new ProcessingLoggerObserver();

        observer.Subscribe(publisher);

        string data = "HelloWorld";

        string processed = context.Execute(data);
        publisher.PublishDataProcessed(processed);

        Console.WriteLine("\n========== СЦЕНАРІЙ 2: ДИНАМІЧНА ЗМІНА ЛОГЕРА ==========");

        LoggerManager.Instance.SetFactory(new FileLoggerFactory());

        processed = context.Execute(data);
        publisher.PublishDataProcessed(processed);

        Console.WriteLine("Перевірте файл log.txt.");

        Console.WriteLine("\n========== СЦЕНАРІЙ 3: ДИНАМІЧНА ЗМІНА СТРАТЕГІЇ ==========");

        context.SetStrategy(new CompressDataStrategy());

        processed = context.Execute(data);
        publisher.PublishDataProcessed(processed);

        Console.WriteLine("Програму завершено.");
    }
}
