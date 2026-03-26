using System.Collections.Generic;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public List<Course> Courses { get; set; } = new List<Course>();

    public Student() { }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }
}