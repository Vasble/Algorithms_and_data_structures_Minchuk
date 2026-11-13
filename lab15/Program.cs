using System;
using System.Collections.Generic;

class Program
{
    static List<string> TopoSort(Dictionary<string, List<string>> before,
                                 Dictionary<string, List<string>> after)
    {
        var incoming = new Dictionary<string, int>();
        // TODO 1
        foreach (var pair in before)
            incoming[pair.Key] = pair.Value.Count;

        var queue = new Queue<string>();
        // TODO 2
        foreach (var pair in incoming)
            if (pair.Value == 0)
                queue.Enqueue(pair.Key);

        var order = new List<string>();
        while (queue.Count > 0)
        {
            string v = queue.Dequeue();
            order.Add(v);
            foreach (string next in after[v])
            {
                // TODO 3
                incoming[next]--;
                if (incoming[next] == 0)
                    queue.Enqueue(next);
            }
        }
        return order;
    }

    // TODO 4
    static bool HasCycle(Dictionary<string, List<string>> before,
                         Dictionary<string, List<string>> after)
    {
        return TopoSort(before, after).Count < before.Count;
    }

    static Dictionary<string, List<string>> Copy(Dictionary<string, List<string>> src)
    {
        var copy = new Dictionary<string, List<string>>();
        foreach (var pair in src)
            copy[pair.Key] = new List<string>(pair.Value);
        return copy;
    }

    static void Report(string name, Dictionary<string, List<string>> before,
                       Dictionary<string, List<string>> after)
    {
        var order = TopoSort(before, after);
        Console.WriteLine("=== Набір " + name + " ===");
        Console.WriteLine("topoSort: " + order.Count + " предметів");
        Console.WriteLine("Порядок: " + string.Join(" → ", order));
        if (order.Count > 0)
        {
            Console.WriteLine("Перший: " + order[0]);
            Console.WriteLine("Останній: " + order[order.Count - 1]);
        }
        int intro = order.IndexOf("Вступ"), web = order.IndexOf("Вебтехнології");
        if (intro >= 0 && web >= 0)
            Console.WriteLine("«Вступ» раніше за «Вебтехнології»: " + (intro < web ? "так" : "ні"));
        Console.WriteLine("hasCycle: " + HasCycle(before, after));

        var missing = new List<string>();
        foreach (var subject in before.Keys)
            if (!order.Contains(subject)) missing.Add(subject);
        Console.WriteLine("Не потрапили в результат: " +
                          (missing.Count == 0 ? "—" : string.Join(", ", missing)));
        Console.WriteLine();
    }

    static void Main()
    {
        var beforeA = new Dictionary<string, List<string>>
        {
            { "Вступ",         new List<string>() },
            { "Математика",    new List<string>() },
            { "ООП",           new List<string> { "Вступ" } },
            { "Бази даних",    new List<string> { "Вступ" } },
            { "Алгоритми",     new List<string> { "Вступ", "Математика" } },
            { "Вебтехнології", new List<string> { "ООП", "Бази даних" } },
            { "Курсовий",      new List<string> { "Алгоритми", "Вебтехнології" } }
        };
        var afterA = new Dictionary<string, List<string>>
        {
            { "Вступ",         new List<string> { "ООП", "Бази даних", "Алгоритми" } },
            { "Математика",    new List<string> { "Алгоритми" } },
            { "ООП",           new List<string> { "Вебтехнології" } },
            { "Бази даних",    new List<string> { "Вебтехнології" } },
            { "Алгоритми",     new List<string> { "Курсовий" } },
            { "Вебтехнології", new List<string> { "Курсовий" } },
            { "Курсовий",      new List<string>() }
        };

        var beforeB = Copy(beforeA);
        beforeB["Бази даних"] = new List<string> { "Вступ", "Вебтехнології" };
        var afterB = Copy(afterA);
        afterB["Вебтехнології"] = new List<string> { "Курсовий", "Бази даних" };

        Report("А", beforeA, afterA);
        Report("Б", beforeB, afterB);
    }
}