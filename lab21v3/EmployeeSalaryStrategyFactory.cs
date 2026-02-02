
public static class EmployeeSalaryStrategyFactory
{
    public static IEmployeeSalaryStrategy CreateStrategy(string employeeLevel)
    {
        switch (employeeLevel)
        {
            case "Junior":
                return new JuniorDeveloper();
            case "Middle":
                return new MiddleDeveloper();
            case "Senior":
                return new SeniorDeveloper();
            default:
                throw new ArgumentException("Invalid employee level");
        }
    }
}
