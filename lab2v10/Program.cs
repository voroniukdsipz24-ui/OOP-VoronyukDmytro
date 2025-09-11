using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Time t1 = new Time(10, 59, 59);
        Time t2 = new Time(11, 0, 0);

        Console.WriteLine($"Час t1 = {t1}");
        Console.WriteLine($"Час t2 = {t2}");

        // Порівняння
        Console.WriteLine($"t1 == t2 ? {(t1 == t2)}");
        Console.WriteLine($"t1 < t2 ? {(t1 < t2)}");
        Console.WriteLine($"t1 > t2 ? {(t1 > t2)}");

        // Інкремент / декремент
        Console.WriteLine($"t1++ → {(t1++)}");
        Console.WriteLine($"++t1 → {(++t1)}");
        Console.WriteLine($"t2-- → {(t2--)}");
        Console.WriteLine($"--t2 → {(--t2)}");

        // Логічні оператори
        Time t3 = new Time(0, 0, 0);
        if (t1)
            Console.WriteLine("t1 не дорівнює півночі (00:00:00)");
        if (t3 == new Time(0, 0, 0))
            Console.WriteLine("t3 дорівнює півночі (00:00:00)");

        // Перевірка операторів + і -
        Time t4 = t1 + 90; // додаємо 90 хв
        Console.WriteLine($"До t1 додали 90 хв → {t4}");

        Time t5 = t4 - 200; // віднімаємо 200 хв
        Console.WriteLine($"Від t4 відняли 200 хв → {t5}");
    }
}

