using System;

// Похідний клас "Собака"
public class Dog : Animal
{
    public string Breed { get; set; }

    // Виклик конструктора базового класу через base(...)
    public Dog(string name, int caloriesPerMeal, int mealsPerDay, string breed)
        : base(name, caloriesPerMeal, mealsPerDay)
    {
        Breed = breed;
    }

    // Перевизначення методу Speak()
    public override void Speak()
    {
        Console.WriteLine($"{Name} (собака) каже: Гав!");
    }

    // Перевизначення Eat()
    public override int Eat()
    {
        Console.WriteLine($"{Name} (собака) їсть {MealsPerDay} раз(и) на день.");
        return base.Eat(); // виклик базового методу
    }
}
