using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class StudentRepository
{
    private List<Student> students = new List<Student>();
    private int nextId = 1;

    public void Add(Student student)
    {
        student.Id = nextId++;
        students.Add(student);
    }

    public List<Student> GetAll()
    {
        return students;
    }

    public Student GetById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public async Task SaveToFileAsync(string filename)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        using FileStream fs = File.Create(filename);
        await JsonSerializer.SerializeAsync(fs, students, options);
    }

    public async Task LoadFromFileAsync(string filename)
    {
        if (!File.Exists(filename))
            return;

        using FileStream fs = File.OpenRead(filename);
        students = await JsonSerializer.DeserializeAsync<List<Student>>(fs)
                   ?? new List<Student>();
    }
}