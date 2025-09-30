using System;

// Похідний клас "Кіт"
public class Cat : Animal
{
    public bool IsIndoor { get; set; }

    public Cat(string name, int caloriesPerMeal, int mealsPerDay, bool isIndoor)
        : base(name, caloriesPerMeal, mealsPerDay)
    {
        IsIndoor = isIndoor;
    }

    // Перевизначення методу Speak()
    public override void Speak()
    {
        Console.WriteLine($"{Name} (кіт) каже: Мяу!");
    }

    // Перевизначення Eat()
    public override int Eat()
    {
        if (IsIndoor)
            Console.WriteLine($"{Name} (домашній кіт) чемно їсть корм.");
        else
            Console.WriteLine($"{Name} (вуличний кіт) також полює на їжу.");
        
        return base.Eat();
    }
}
