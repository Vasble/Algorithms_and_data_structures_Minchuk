using System;
using System.Collections.Generic;

class Product
{
    public string Code { get; }   
    public string Name { get; set; } 

    public Product(string code, string name) { Code = code; Name = name; }

    public override bool Equals(object? obj) =>
        obj is Product other && Code == other.Code;
        
    public override int GetHashCode() => Code.GetHashCode();
}

class Program
{
    static void Main()
    {
        var items = new List<Product>
        {
            new("A-100", "Кабель USB-C"), new("B-205", "Миша бездротова"),
            new("A-100", "Кабель USB-C"), new("C-310", "Клавіатура"),
            new("D-415", "Монітор 24 дюйми"), new("B-205", "Миша бездротова"),
            new("E-520", "Навушники"), new("F-630", "Вебкамера"),
        };

        var unique = new HashSet<Product>();
        foreach (var p in items) unique.Add(p);

        var stock = new Dictionary<Product, int>();
        stock[new Product("A-100", "Кабель USB-C")] = 17;
        stock[new Product("A-100", "Кабель USB-C")] = 25;

        var a = new Product("A-100", "Кабель USB-C");
        var b = new Product("A-100", "Кабель USB-C");
        Console.WriteLine($"(1) a.Equals(b)           = {a.Equals(b)}");
        Console.WriteLine($"(2) unique.Count          = {unique.Count}");
        Console.WriteLine($"(3) unique.Contains(a)    = {unique.Contains(a)}");
        Console.WriteLine($"(4) stock.Count           = {stock.Count}");
        Console.WriteLine($"(5) stock.ContainsKey(a)  = {stock.ContainsKey(a)}");
    }
}