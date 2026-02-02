
public class JuniorDeveloper : IEmployeeSalaryStrategy
{
    public decimal CalculateSalary(decimal hoursWorked, int tasksCompleted)
    {
        decimal hourlyRate = 15.0m; // Погодинна ставка
        decimal taskBonus = 50.0m;  // Бонус за кожне закрите завдання

        return (hoursWorked * hourlyRate) + (tasksCompleted * taskBonus);
    }
}
