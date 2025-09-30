using System;

// Базовий клас "Тварина"
public class Animal
{
    public string Name { get; set; }
    public int CaloriesPerMeal { get; set; }   // калорії за одне приймання їжі
    public int MealsPerDay { get; set; }       // кількість прийомів їжі на день

    // Конструктор базового класу
    public Animal(string name, int caloriesPerMeal, int mealsPerDay)
    {
        Name = name;
        CaloriesPerMeal = caloriesPerMeal;
        MealsPerDay = mealsPerDay;
    }

    // Віртуальний метод — буде перевизначатися
    public virtual void Speak()
    {
        Console.WriteLine($"{Name} видає звук.");
    }

    // Віртуальний метод розрахунку калорій
    public virtual int Eat()
    {
        return CaloriesPerMeal * MealsPerDay;
    }
}
