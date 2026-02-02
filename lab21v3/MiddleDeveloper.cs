
public class MiddleDeveloper : IEmployeeSalaryStrategy
{
    public decimal CalculateSalary(decimal hoursWorked, int tasksCompleted)
    {
        decimal hourlyRate = 25.0m; // Погодинна ставка
        decimal taskBonus = 100.0m; // Бонус за кожне закрите завдання

        return (hoursWorked * hourlyRate) + (tasksCompleted * taskBonus);
    }
}
