using System;
using System.Collections.Generic;
using System.Text;

class Request
{
    public string Name;
    public int Minutes; 
    public Request(string name, int minutes) { Name = name; Minutes = minutes; }
}

class Program
{
    static readonly string[] lines =
    {
        "",
        "a + b",
        "{[a] + (b)}",
        "([)]",
        "a)",
        "(a[b]",
        "f(x[1]) * {y + (z)}",
    };

    static readonly List<Request> requests = new List<Request>
    {
        new Request("Довідка про навчання", 4),
        new Request("Академічна відпустка", 7),
        new Request("Копія наказу", 2),
        new Request("Виписка з оцінками", 6),
        new Request("Перескладання", 9),
        new Request("Студентський квиток", 3),
    };

    // TODO 1
    static bool IsBalanced(string line)
    {
        var stack = new Stack<char>();
        foreach (char c in line)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0) return false;
                char open = stack.Pop();
                if (!((open == '(' && c == ')') ||
                      (open == '[' && c == ']') ||
                      (open == '{' && c == '}')))
                    return false;
            }
        }
        return stack.Count == 0; 
    }

    // TODO 2
    static int ProcessQueue(IEnumerable<Request> input)
    {
        var queue = new Queue<Request>(input);
        if (queue.Count == 0)
        {
            Console.WriteLine("Черга порожня: обробляти нічого.");
            return 0;
        }

        int time = 0, count = 0, totalWait = 0;
        while (queue.Count > 0)
        {
            Request r = queue.Dequeue();
            count++;
            int start = time;
            int end = start + r.Minutes;
            Console.WriteLine($"{count}. {r.Name} з {start} до {end} хв");
            totalWait += start; 
            time = end;
        }
        Console.WriteLine($"Оброблено заявок: {count}, загальний час: {time} хв");
        Console.WriteLine($"Середній час очікування: {(double)totalWait / count} хв");
        return time;
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== Задача 1. Перевірка дужок ===");
        int ok = 0;
        foreach (string line in lines)
        {
            bool b = IsBalanced(line);
            if (b) ok++;
            string shown = line.Length == 0 ? "(порожній рядок)" : line;
            Console.WriteLine($"{shown} -> {(b ? "так" : "ні")}");
        }
        Console.WriteLine($"Збалансованих: {ok}");

        Console.WriteLine();
        Console.WriteLine("=== Задача 2. Обробка заявок ===");
        ProcessQueue(requests);

        Console.WriteLine();
        Console.WriteLine("--- Порожня черга ---");
        ProcessQueue(new List<Request>());
    }
    
}