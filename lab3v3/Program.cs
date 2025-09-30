using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Колекція з базовим типом — демонстрація поліморфізму
        List<Animal> animals = new List<Animal>()
        {
            new Dog("Рекс", 300, 3, "Вівчарка"),
            new Cat("Мурка", 200, 4, true),
            new Dog("Бадді", 400, 2, "Бульдог"),
            new Cat("Барсик", 250, 3, false)
        };

        Console.WriteLine("=== Симуляція тварин ===\n");

        foreach (var animal in animals)
        {
            animal.Speak();
            int dailyCalories = animal.Eat();
            Console.WriteLine($"{animal.Name} споживає {dailyCalories} калорій на день.\n");
        }

        // Порівняння калорій
        Console.WriteLine("=== Порівняння тварин за калоріями ===");
        animals.Sort((a, b) => b.Eat().CompareTo(a.Eat()));

        foreach (var animal in animals)
        {
            Console.WriteLine($"{animal.Name} → {animal.Eat()} калорій/день");
        }
    }
}
