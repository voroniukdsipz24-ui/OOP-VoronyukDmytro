namespace lab24.Observer;
public class HistoryLoggerObserver
{
    public List<string> History { get; } = new();
    public void Subscribe(ResultPublisher publisher)
    {
        publisher.ResultCalculated += OnResultCalculated;
    }
    private void OnResultCalculated(double result, string operation)
    {
        History.Add($"{operation}: {result}");
    }
}
