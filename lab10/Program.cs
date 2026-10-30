using System.Text;

namespace Lab10;

public record Request(string Name, int Priority, int Seq);

public static class Program
{
    private static readonly List<Request> Incoming = new()
    {
        new Request("Оновити підпис у листі", 3, 1),
        new Request("Сервер не відповідає", 1, 2),
        new Request("Не друкує принтер", 2, 3),
        new Request("Не працює термінал оплати", 1, 4),
        new Request("Змінити шпалери", 3, 5),
    };

    private static readonly Request Late = new("Відвалився інтернет у відділі", 1, 6);

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- Задача 1: ключ = priority ---");
        ProcessAll(Incoming);

        Console.WriteLine();
        Console.WriteLine("--- Задача 2: ключ = (priority, seq) ---");
        ProcessStable(Incoming);

        Console.WriteLine();
        Console.WriteLine("--- Задача 3: заявка late надходить після двох оброблених ---");
        ProcessWithLate(Incoming, Late);
    }

    private static void Print(int order, Request r) =>
        Console.WriteLine($"{order}. {r.Name} | пріоритет {r.Priority} | seq {r.Seq}");

    // ---------- Задача 1 ----------
    public static void ProcessAll(IEnumerable<Request> incoming)
    {
        var queue = new PriorityQueue<Request, int>();
        foreach (var r in incoming)
        {
            queue.Enqueue(r, r.Priority);
        }

        int order = 1;
        while (queue.Count > 0)
        {
            Print(order++, queue.Dequeue());
        }
    }

    // ---------- Задача 2 ----------
    public static void ProcessStable(IEnumerable<Request> incoming)
    {
        var queue = new PriorityQueue<Request, (int Priority, int Seq)>();
        foreach (var r in incoming)
        {
            queue.Enqueue(r, (r.Priority, r.Seq));
        }

        int order = 1;
        while (queue.Count > 0)
        {
            Print(order++, queue.Dequeue());
        }
    }

    // ---------- Задача 3 ----------
    public static void ProcessWithLate(IEnumerable<Request> incoming, Request late)
    {
        var queue = new PriorityQueue<Request, (int Priority, int Seq)>();
        foreach (var r in incoming)
        {
            queue.Enqueue(r, (r.Priority, r.Seq));
        }

        int order = 1;
        for (int i = 0; i < 2 && queue.Count > 0; i++)
        {
            Print(order++, queue.Dequeue());
        }

        queue.Enqueue(late, (late.Priority, late.Seq));

        while (queue.Count > 0)
        {
            Print(order++, queue.Dequeue());
        }
    }
}