namespace lab24.Strategy;
public class SquareRootOperationStrategy : INumericOperationStrategy
{
    public string Name => "Square Root";
    public double Execute(double value)
    {
        if (value < 0)
            throw new ArgumentException("Cannot calculate square root of negative number.");
        return Math.Sqrt(value);
    }
}
