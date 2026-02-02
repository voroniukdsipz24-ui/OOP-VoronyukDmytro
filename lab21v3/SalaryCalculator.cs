
public class SalaryCalculator
{
    public decimal CalculateSalary(decimal hoursWorked, int tasksCompleted, IEmployeeSalaryStrategy strategy)
    {
        return strategy.CalculateSalary(hoursWorked, tasksCompleted);
    }
}
