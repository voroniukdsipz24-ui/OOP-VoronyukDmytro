using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        StudentRepository repo = new StudentRepository();

        Course math = new Course(1, "Математика");
        Course prog = new Course(2, "Програмування");

        Student s1 = new Student("Іван", 20);
        s1.Courses.Add(math);
        s1.Courses.Add(prog);

        Student s2 = new Student("Олена", 19);
        s2.Courses.Add(prog);

        repo.Add(s1);
        repo.Add(s2);

        Console.WriteLine("Створено студентів.");

        await repo.SaveToFileAsync("students.json");

        Console.WriteLine("Дані успішно збережено у файл JSON.");

        StudentRepository repo2 = new StudentRepository();

        await repo2.LoadFromFileAsync("students.json");

        Console.WriteLine("\nДані завантажено з файлу:\n");

        foreach (var student in repo2.GetAll())
        {
            Console.WriteLine($"ID: {student.Id}");
            Console.WriteLine($"Ім'я: {student.Name}");
            Console.WriteLine($"Вік: {student.Age}");

            Console.WriteLine("Курси:");

            foreach (var course in student.Courses)
            {
                Console.WriteLine($" - {course.Name}");
            }

            Console.WriteLine();
        }
    }
}