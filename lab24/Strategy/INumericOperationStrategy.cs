namespace lab24.Strategy;
public interface INumericOperationStrategy
{
    double Execute(double value);
    string Name { get; }
}
