using System;

class Program
{
    static void Main(string[] args)
    {
        string employeeLevel = "";
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Повторюємо введення рівня, поки не введено правильний рівень
        while (employeeLevel != "Junior" && employeeLevel != "Middle" && employeeLevel != "Senior")
        {
            Console.WriteLine("Введіть рівень співробітника (Junior, Middle, Senior): ");
            employeeLevel = Console.ReadLine();

            // Якщо введено неправильний рівень, пропонуємо ввести ще раз
            if (employeeLevel != "Junior" && employeeLevel != "Middle" && employeeLevel != "Senior")
            {
                Console.WriteLine("Невірний рівень співробітника. Спробуйте ще раз.");
            }
        }

        decimal hoursWorked;
        int tasksCompleted;

        // Перевірка на введення чисел для відпрацьованих годин
        while (true)
        {
            Console.WriteLine("Введіть кількість відпрацьованих годин: ");
            if (decimal.TryParse(Console.ReadLine(), out hoursWorked))
                break;
            else
                Console.WriteLine("Будь ласка, введіть коректну кількість відпрацьованих годин.");
        }

        // Перевірка на введення чисел для кількості закритих завдань
        while (true)
        {
            Console.WriteLine("Введіть кількість закритих завдань: ");
            if (int.TryParse(Console.ReadLine(), out tasksCompleted))
                break;
            else
                Console.WriteLine("Будь ласка, введіть коректну кількість закритих завдань.");
        }

        // Створення стратегії на основі введеного рівня співробітника
        IEmployeeSalaryStrategy strategy = EmployeeSalaryStrategyFactory.CreateStrategy(employeeLevel);
        
        // Розрахунок зарплати
        SalaryCalculator calculator = new SalaryCalculator();
        decimal salary = calculator.CalculateSalary(hoursWorked, tasksCompleted, strategy);

        Console.WriteLine($"Зарплата співробітника: {salary} грн");
    }
}
