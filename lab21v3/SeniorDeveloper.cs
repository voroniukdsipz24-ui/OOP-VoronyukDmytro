
public class SeniorDeveloper : IEmployeeSalaryStrategy
{
    public decimal CalculateSalary(decimal hoursWorked, int tasksCompleted)
    {
        decimal hourlyRate = 40.0m; // Погодинна ставка
        decimal taskBonus = 150.0m; // Бонус за кожне закрите завдання

        return (hoursWorked * hourlyRate) + (tasksCompleted * taskBonus);
    }
}
