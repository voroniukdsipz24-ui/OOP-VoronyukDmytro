
# Factory vs AbstractFactory

**Factory Method** та **Abstract Factory** є двома патернами проектування, які використовуються для створення об'єктів, але мають різні підходи та цілі.

## Різниця між Factory Method та Abstract Factory

### 1. **Factory Method**:
   Це патерн, який визначає інтерфейс для створення об'єкта, але дозволяє підкласам змінювати тип створюваного об'єкта. Тобто Factory Method дозволяє делегувати створення об'єкта підкласам. Зазвичай він використовується для створення одного типу об'єкта в межах певної ієрархії класів.

   **Приклад**:
   Уявімо, що є базовий клас `Product`, а для різних типів продуктів, таких як `Book` і `DVD`, ми маємо відповідні класи, що створюються через Factory Method:

   ```c#
using System;

// Абстрактний клас продукту
public abstract class Product
{
    public abstract void Display();
}

// Клас продукту "Книга"
public class Book : Product
{
    public override void Display()
    {
        Console.WriteLine("This is a Book.");
    }
}

// Клас продукту "DVD"
public class DVD : Product
{
    public override void Display()
    {
        Console.WriteLine("This is a DVD.");
    }
}

// Фабрика продуктів
public class ProductFactory
{
    public Product CreateProduct(string productType)
    {
        if (productType == "book")
        {
            return new Book();
        }
        else if (productType == "dvd")
        {
            return new DVD();
        }
        else
        {
            throw new ArgumentException("Unknown product type.");
        }
    }
}

// Основна програма
class Program
{
    static void Main(string[] args)
    {
        ProductFactory factory = new ProductFactory();
        
        // Створення продуктів через фабрику
        Product product1 = factory.CreateProduct("book");
        product1.Display();
        
        Product product2 = factory.CreateProduct("dvd");
        product2.Display();
    }
}
   ```

### 2. **Abstract Factory**:
   Це більш абстрактний патерн, який надає інтерфейс для створення родини взаємозалежних або взаємозамінних об'єктів без вказування їх конкретних класів. Abstract Factory створює не один об'єкт, а цілу родину продуктів, що мають спільну тему (наприклад, різні типи інтерфейсів користувача для різних операційних систем).

   **Приклад**:
   Уявімо, що ми маємо різні інтерфейси користувача для Windows та macOS. Використовуючи Abstract Factory, ми можемо створити відповідні елементи для кожної операційної системи:

   ```c#
using System;

// Абстрактний клас кнопки
public abstract class Button
{
    public abstract void Render();
}

// Клас кнопки для Windows
public class WinButton : Button
{
    public override void Render()
    {
        Console.WriteLine("Rendering Windows button.");
    }
}

// Клас кнопки для macOS
public class MacButton : Button
{
    public override void Render()
    {
        Console.WriteLine("Rendering Mac button.");
    }
}

// Абстрактна фабрика для створення кнопок
public abstract class GUIFactory
{
    public abstract Button CreateButton();
}

// Фабрика для Windows
public class WinFactory : GUIFactory
{
    public override Button CreateButton()
    {
        return new WinButton();
    }
}

// Фабрика для macOS
public class MacFactory : GUIFactory
{
    public override Button CreateButton()
    {
        return new MacButton();
    }
}

// Основна програма
class Program
{
    static void Main(string[] args)
    {
        GUIFactory factory;
        
        // Створення фабрики для Windows
        factory = new WinFactory();
        Button winButton = factory.CreateButton();
        winButton.Render();
        
        // Створення фабрики для macOS
        factory = new MacFactory();
        Button macButton = factory.CreateButton();
        macButton.Render();
    }
}
   ```

## Коли використовувати кожен патерн?

- **Factory Method** доцільно використовувати, коли:
  - Потрібно створювати об'єкти різних типів, але з однаковим інтерфейсом.
  - Кожен продукт може бути створений за допомогою конкретного підкласу без необхідності змінювати базову логіку.
  - Якщо є потреба в розширенні продуктів, можна додати нові класи без змін у наявному коді.

- **Abstract Factory** доцільно використовувати, коли:
  - Необхідно створювати не один об'єкт, а цілу родину взаємозалежних об'єктів.
  - Потрібно забезпечити сумісність об'єктів у родині продуктів.
  - Є потреба в розширенні функціональності, що включає створення об'єктів, залежних від певного контексту (наприклад, теми інтерфейсу користувача).

## Переваги та недоліки

- **Factory Method**:
  - **Переваги**:
    - Простота реалізації.
    - Легко змінювати типи продуктів без зміни основного коду.
    - Можна додавати нові типи продуктів без змін у Factory.
  - **Недоліки**:
    - Може стати складним, якщо потрібно створювати дуже багато різних типів об'єктів.
    - Ускладнює роботу з продуктами, якщо їх є багато і вони сильно варіативні.

- **Abstract Factory**:
  - **Переваги**:
    - Забезпечує високий рівень абстракції.
    - Ідеально підходить для роботи з групами взаємозалежних продуктів.
    - Спрощує керування взаємодією між продуктами різних родин.
  - **Недоліки**:
    - Складніша реалізація, ніж у Factory Method.
    - Збільшення кількості класів і їх зв'язків, що може ускладнити підтримку та розширення.

## Висновок

**Factory Method** і **Abstract Factory** - це потужні інструменти в проектуванні програм, які допомагають абстрагувати створення об'єктів. Вибір між ними залежить від конкретної ситуації та вимог. Якщо потрібно створювати лише один тип об'єктів, то Factory Method буде простим і зручним рішенням. Якщо ж передбачається створення різних груп об'єктів, які повинні бути сумісними, то краще використовувати Abstract Factory.
