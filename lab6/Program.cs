using System;
using System.Collections.Generic;
using System.Linq;

class Category
{
    public string Name;
    public int Products;             
    public List<Category> Children;

    public Category(string name, int products, List<Category> children)
    {
        Name = name; Products = products; Children = children;
    }
}

class Program
{
    static int callCount = 0;    

    static Category catalog = new Category("Каталог", 0, new List<Category> {
        new Category("Одяг", 0, new List<Category> {
            new Category("Верхній одяг", 0, new List<Category> {
                new Category("Куртки", 12, new List<Category>()),
                new Category("Пальта", 7, new List<Category>()),
            }),
            new Category("Светри", 15, new List<Category>()),
        }),
        new Category("Взуття", 4, new List<Category> {
            new Category("Кросівки", 23, new List<Category>()),
            new Category("Чоботи", 9, new List<Category>()),
        }),
        new Category("Аксесуари", 0, new List<Category> {
            new Category("Сумки", 18, new List<Category>()),
            new Category("Ремені", 5, new List<Category>()),
        }),
    });

    // 1
    static void PrintTree(Category node, int level)
    {
        callCount++;
        Console.WriteLine(new string(' ', level * 2) + node.Name);
        foreach (var child in node.Children)
            PrintTree(child, level + 1);
    }

    // 2
    static int CountProducts(Category node)
    {
        callCount++;
        if (node.Children.Count == 0)    
            return node.Products;
        int total = node.Products;
        foreach (var child in node.Children)
            total += CountProducts(child);
        return total;
    }

    // 3
    static int MaxDepth(Category node)
    {
        callCount++;
        if (node.Children.Count == 0)    
            return 1;
        return 1 + node.Children.Max(c => MaxDepth(c));
    }

    static void Main()
    {
        PrintTree(catalog, 0);
        Console.WriteLine("printTree викликів: " + callCount);

        callCount = 0;
        Console.WriteLine("Товарів: " + CountProducts(catalog));
        Console.WriteLine("countProducts викликів: " + callCount);

        callCount = 0;
        Console.WriteLine("Глибина: " + MaxDepth(catalog));
    }
}