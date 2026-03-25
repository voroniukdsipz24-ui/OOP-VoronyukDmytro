# Code Smells та рефакторинг: практичний аналіз

## Вступ

Рефакторинг — це процес покращення внутрішньої структури програмного коду без зміни його зовнішньої поведінки. Основною метою рефакторингу є підвищення читабельності, зменшення складності, покращення підтримки та спрощення подальшого розвитку програмного забезпечення.

У процесі розробки програм часто виникають так звані **code smells (запахи коду)** — ознаки того, що структура програми має проблеми. Code smell не є помилкою, але він ускладнює підтримку, тестування та розширення коду.

У цій роботі буде виконано практичний аналіз коду з попередніх лабораторних робіт та знайдено декілька code smells, після чого буде показано їх виправлення за допомогою рефакторингу.

---

## Code smell №1 — Long Method

### Проблема

Метод виконує занадто багато дій і має велику кількість рядків.

### Код ДО

```csharp
public void ProcessOrder(Order order)
{
    if (order == null)
        throw new Exception();

    if (order.Items.Count == 0)
        throw new Exception();

    decimal total = 0;

    foreach (var item in order.Items)
    {
        total += item.Price * item.Quantity;
    }

    Console.WriteLine("Total: " + total);

    SaveOrder(order);

    SendEmail(order);
}
```

### Code smell

* Long Method
* Порушення принципу SRP

### Рефакторинг

Техніка: **Extract Method**

### Код ПІСЛЯ

```csharp
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    decimal total = CalculateTotal(order);
    PrintTotal(total);
    SaveOrder(order);
    SendEmail(order);
}

private void ValidateOrder(Order order)
{
    if (order == null)
        throw new Exception();

    if (order.Items.Count == 0)
        throw new Exception();
}

private decimal CalculateTotal(Order order)
{
    decimal total = 0;

    foreach (var item in order.Items)
    {
        total += item.Price * item.Quantity;
    }

    return total;
}

private void PrintTotal(decimal total)
{
    Console.WriteLine("Total: " + total);
}
```

### Результат

Код став коротшим, зрозумілішим і легшим для тестування.

---

## Code smell №2 — Duplicate Code

### Проблема

Однакова формула використовується в різних методах.

### Код ДО

```csharp
public decimal GetRetailPrice(decimal price)
{
    return price * 1.2m;
}

public decimal GetWholesalePrice(decimal price)
{
    return price * 1.2m * 0.9m;
}
```

### Code smell

* Duplicate Code
* Magic numbers

### Рефакторинг

Техніка: **Extract Method + Extract Constant**

### Код ПІСЛЯ

```csharp
private const decimal Markup = 1.2m;
private const decimal Discount = 0.9m;

private decimal ApplyMarkup(decimal price)
{
    return price * Markup;
}

public decimal GetRetailPrice(decimal price)
{
    return ApplyMarkup(price);
}

public decimal GetWholesalePrice(decimal price)
{
    return ApplyMarkup(price) * Discount;
}
```

### Результат

* Менше дублювання
* Легше змінювати логіку
* Код зрозуміліший

---

## Code smell №3 — Long Parameter List

### Проблема

Метод приймає багато параметрів.

### Код ДО

```csharp
public void CreateUser(
    string name,
    string email,
    string phone,
    string street,
    string city,
    string zip)
{
}
```

### Code smell

* Long Parameter List
* Primitive Obsession

### Рефакторинг

Техніка: **Introduce Parameter Object**

### Код ПІСЛЯ

```csharp
public class Address
{
    public string Street;
    public string City;
    public string Zip;
}

public class UserInfo
{
    public string Name;
    public string Email;
    public string Phone;
    public Address Address;
}

public void CreateUser(UserInfo user)
{
}
```

### Результат

* Метод став простіший
* Легше додавати нові поля
* Код більш обʼєктно-орієнтований

---

## Чому рефакторинг без тестів є небезпечним?

Перед рефакторингом необхідно мати тести, тому що рефакторинг змінює структуру коду, і можна випадково зламати логіку.

### Приклад

Було:

```csharp
decimal GetPrice(int q, decimal p)
{
    return q * p;
}
```

Після рефакторингу:

```csharp
decimal GetPrice(int q, decimal p)
{
    return q + p;
}
```

Код компілюється, але працює неправильно.

Якщо є тести:

```
GetPrice(2, 10) -> 20
```

Тест покаже помилку.

Тому правило:

> Рефакторинг без тестів — це ризик.

---

Ок, ось **відповіді на контрольні питання з лекції 24**, нормально оформлені, щоб можна було вставити в кінець роботи або окремим розділом.

---

## Контрольні питання

### 1. Що таке рефакторинг і навіщо він потрібен?

Рефакторинг — це процес покращення внутрішньої структури програмного коду без зміни його зовнішньої поведінки. Під час рефакторингу код переписується таким чином, щоб він став більш зрозумілим, простішим та зручнішим для підтримки.

Рефакторинг потрібен для:

* підвищення читабельності коду;
* зменшення складності програми;
* спрощення виправлення помилок;
* полегшення додавання нової функціональності;
* покращення архітектури програми.

Регулярний рефакторинг дозволяє підтримувати код у чистому стані та уникати накопичення технічного боргу.

---

### 2. Назвіть 5 найпоширеніших code smells та способи їх усунення

1. **Long Method** — занадто довгий метод
   Рішення: Extract Method (розбиття на менші методи)

2. **Large Class (God Object)** — клас має занадто багато відповідальностей
   Рішення: Extract Class (розділення на декілька класів)

3. **Duplicate Code** — дублювання коду
   Рішення: Extract Method або винесення спільної логіки

4. **Long Parameter List** — занадто багато параметрів у методі
   Рішення: Introduce Parameter Object

5. **Magic Numbers** — використання чисел без пояснення
   Рішення: Extract Constant (використання іменованих констант)

Додаткові приклади:

* Primitive Obsession
* Switch Statements
* Dead Code

---

### 3. Що таке “God Object” і як його виправити?

God Object (або Large Class) — це клас, який має занадто багато методів, полів та відповідальностей. Такий клас стає центральним у системі і залежить від багатьох інших компонентів.

Проблеми God Object:

* важко читати;
* важко тестувати;
* важко змінювати;
* порушення принципу SRP (Single Responsibility Principle).

Спосіб виправлення:

Техніка рефакторингу — Extract Class.

Великий клас потрібно розділити на декілька менших класів, кожен з яких виконує одну задачу.

Наприклад:

Було:

* OrderManager робить все

Стало:

* OrderValidator
* OrderRepository
* EmailService
* ReportService

Це робить систему більш гнучкою.

---

### 4. Чому важливо мати тести перед рефакторингом?

Перед рефакторингом необхідно мати тести, тому що рефакторинг змінює структуру коду, і є ризик випадково змінити поведінку програми.

Тести дозволяють:

* перевірити, що програма працює правильно;
* швидко знайти помилки;
* безпечно виконувати зміни;
* контролювати якість коду.

Без тестів рефакторинг може призвести до:

* нових багів;
* неправильної логіки;
* поломки програми.

Тому правильний процес рефакторингу:

1. Написати або перевірити тести
2. Зробити маленьку зміну
3. Запустити тести
4. Зробити commit
5. Повторити

---

### 5. Що таке цикломатична складність і як вона впливає на якість коду?

Цикломатична складність — це метрика, яка показує кількість незалежних шляхів виконання через код.

Вона залежить від:

* if
* switch
* циклів
* умовних операторів

Чим більше умов, тим складніший код.

Оцінка складності:

| Значення | Оцінка                             |
| -------- | ---------------------------------- |
| 1–10     | проста функція                     |
| 11–20    | середня складність                 |
| 20+      | складний код, потрібен рефакторинг |

Велика цикломатична складність призводить до:

* складного тестування
* великої кількості помилок
* поганої читабельності
* важкої підтримки

Тому складні методи потрібно розбивати на менші.

---

## Висновок

У ході роботи було розглянуто процес рефакторингу та знайдено декілька поширених code smells:

* Long Method
* Duplicate Code
* Long Parameter List

Для їх усунення були застосовані техніки:

* Extract Method
* Extract Constant
* Introduce Parameter Object

Рефакторинг дозволяє зробити код більш зрозумілим, простим у підтримці та готовим до розширення. Також було показано, що виконувати рефакторинг без тестів небезпечно, тому що можна змінити поведінку програми. Отже, регулярний рефакторинг є важливою частиною професійної розробки програмного забезпечення.