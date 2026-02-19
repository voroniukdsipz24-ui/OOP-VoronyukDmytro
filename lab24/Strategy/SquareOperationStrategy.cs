namespace lab24.Strategy;
public class SquareOperationStrategy : INumericOperationStrategy
{
    public string Name => "Square";
    public double Execute(double value) => value * value;
}
