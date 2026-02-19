using lab24.Strategy;
using lab24.Observer;

namespace lab24;

class Program
{
    static void Main()
    {
        var processor = new NumericProcessor(new SquareOperationStrategy());
        var publisher = new ResultPublisher();

        var consoleObserver = new ConsoleLoggerObserver();
        var historyObserver = new HistoryLoggerObserver();
        var thresholdObserver = new ThresholdNotifierObserver(50);

        consoleObserver.Subscribe(publisher);
        historyObserver.Subscribe(publisher);
        thresholdObserver.Subscribe(publisher);

        double[] numbers = { 4, 9, 16 };

        INumericOperationStrategy[] strategies =
        {
            new SquareOperationStrategy(),
            new CubeOperationStrategy(),
            new SquareRootOperationStrategy()
        };

        foreach (var strategy in strategies)
        {
            processor.SetStrategy(strategy);
            Console.WriteLine($"\n--- Strategy: {strategy.Name} ---");

            foreach (var number in numbers)
            {
                try
                {
                    double result = processor.Process(number);
                    publisher.PublishResult(result, processor.CurrentOperationName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        Console.WriteLine("\n=== History ===");
        foreach (var entry in historyObserver.History)
        {
            Console.WriteLine(entry);
        }
    }
}
