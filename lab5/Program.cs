using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Surname { get; }
    public string Group { get; }
    public int Grade { get; }
    public int Year { get; }
    public Student(string surname, string group, int grade, int year)
    { Surname = surname; Group = group; Grade = grade; Year = year; }
}

class UkrainianComparer : IComparer<string>
{
    private const string Alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
    public int Compare(string a, string b)
    {
        a = a.ToLowerInvariant(); b = b.ToLowerInvariant();
        int n = Math.Min(a.Length, b.Length);
        for (int i = 0; i < n; i++)
        {
            int cmp = Index(a[i]).CompareTo(Index(b[i]));
            if (cmp != 0) return cmp;
        }
        return a.Length.CompareTo(b.Length);
    }
    private static int Index(char c)
    {
        int i = Alphabet.IndexOf(c);
        return i >= 0 ? i : Alphabet.Length;
    }
}

class Program
{
    static void PrintAll(string title, IEnumerable<Student> items) 
    {
        Console.WriteLine(title);
        foreach (var s in items)
            Console.WriteLine($"{s.Surname} {s.Group} {s.Grade} {s.Year}");
        Console.WriteLine();
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var students = new List<Student>
        {
            new Student("Ткаченко", "ІПЗ-3/1", 85, 2024),
            new Student("Бондар", "ІПЗ-3/2", 92, 2023),
            new Student("Іваненко", "ІПЗ-3/1", 85, 2024),
            new Student("Коваль", "ІПЗ-3/2", 78, 2024),
            new Student("Сидоренко", "ІПЗ-3/1", 85, 2023),
            new Student("Мельник", "ІПЗ-3/2", 92, 2024),
            new Student("Гриценко", "ІПЗ-3/1", 78, 2023),
            new Student("Дяченко", "ІПЗ-3/2", 85, 2023),
        };
        var uk = new UkrainianComparer();

        PrintAll("1. За прізвищем", students.OrderBy(s => s.Surname, uk).ToList());

        PrintAll("2. За балом (спадання)", students.OrderByDescending(s => s.Grade).ToList());

        PrintAll("3. За групою, потім за балом",
            students.OrderBy(s => s.Group, StringComparer.Ordinal)
                    .ThenByDescending(s => s.Grade).ToList());

        PrintAll("Частина 2",
            students.OrderByDescending(s => s.Grade)
                    .ThenBy(s => s.Surname, uk).ToList());
    }
}