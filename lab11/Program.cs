using System;
using System.Collections.Generic;

class Node
{
    public string Title;
    public List<Node> Children;

    public Node(string title, List<Node> children)
    {
        Title = title;
        Children = children;
    }
}

class Program
{
    static int callCount = 0; 

    // TODO 1
    static void PrintMenu(Node node, int level)
    {
        Console.WriteLine(new string(' ', level * 2) + node.Title); 
        foreach (Node child in node.Children)                    
        {
            PrintMenu(child, level + 1);
        }
    }

    // TODO 2
    static int CountItems(Node node)
    {
        callCount++;
        int total = 1;                
        foreach (Node child in node.Children)
        {
            total += CountItems(child);    
        }
        return total;
    }

    // TODO 3
    static bool Find(Node node, string name)
    {
        if (node.Title == name)           
            return true;
        foreach (Node child in node.Children)
        {
            if (Find(child, name))
                return true;
        }
        return false;
    }

    static int Depth(Node node)
    {
        int max = 0;
        foreach (Node child in node.Children)
        {
            max = Math.Max(max, Depth(child));
        }
        return max + 1;
    }

    static int CountLeaves(Node node)
    {
        if (node.Children.Count == 0)
            return 1;
        int leaves = 0;
        foreach (Node child in node.Children)
        {
            leaves += CountLeaves(child);
        }
        return leaves;
    }

    static void Main()
    {
        Node menu = new Node("Головне меню", new List<Node>
        {
            new Node("Файл", new List<Node>
            {
                new Node("Новий", new List<Node>()),
                new Node("Зберегти", new List<Node>())
            }),
            new Node("Правка", new List<Node>
            {
                new Node("Копіювати", new List<Node>())
            }),
            new Node("Вигляд", new List<Node>
            {
                new Node("Масштаб", new List<Node>
                {
                    new Node("Збільшити", new List<Node>()),
                    new Node("Зменшити", new List<Node>())
                })
            }),
            new Node("Довідка", new List<Node>())
        });

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== printMenu ===");
        PrintMenu(menu, 0);

        Console.WriteLine();
        Console.WriteLine("=== Контрольна таблиця ===");
        Console.WriteLine("countItems(menu)           = " + CountItems(menu));
        Console.WriteLine("find(\"Зберегти\")           = " + Find(menu, "Зберегти"));
        Console.WriteLine("find(\"Масштаб\")            = " + Find(menu, "Масштаб"));
        Console.WriteLine("find(\"Довідка\")            = " + Find(menu, "Довідка"));
        Console.WriteLine("find(\"Експорт\")            = " + Find(menu, "Експорт"));
        Console.WriteLine("Глибина найдовшої гілки    = " + Depth(menu));
        Console.WriteLine("Пунктів без вкладених      = " + CountLeaves(menu));
        Console.WriteLine("callCount (викликів countItems) = " + callCount);
    }
}