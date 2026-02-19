namespace lab24.Strategy;
public class NumericProcessor
{
    private INumericOperationStrategy _strategy;
    public NumericProcessor(INumericOperationStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }
    public void SetStrategy(INumericOperationStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }
    public double Process(double input)
    {
        return _strategy.Execute(input);
    }
    public string CurrentOperationName => _strategy.Name;
}
