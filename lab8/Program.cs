using System;
using System.Collections.Generic;

record Client(int Id, string Name);
record Order(int Number, int ClientId, int Sum);

class Program
{
    static void Main()
    {
        var clients = new List<Client>
        {
            new(101, "Коваленко"), new(102, "Мельник"), new(103, "Шевчук"),
            new(104, "Бондар"),    new(105, "Ткачук"),  new(106, "Гриценко"),
        };

        var orders = new List<Order>
        {
            new(1, 103, 450), new(2, 101, 120),
            new(3, 105, 980), new(4, 103, 300),
            new(5, 102, 75),  new(6, 106, 1500),
            new(7, 101, 220), new(8, 103, 60),
        };

        var blocked = new List<int> { 102, 104 }; // заблоковані клієнти

        // ---------------------------------------------------------
        // Фрагмент 1. Ім'я клієнта для кожного замовлення

        Console.WriteLine("--- Фрагмент 1 ---");
        var byId = new Dictionary<int, Client>();
        foreach (var client in clients)
            byId[client.Id] = client;
        foreach (var order in orders) // порядок задає список orders
            Console.WriteLine($"Замовлення {order.Number} — {byId[order.ClientId].Name}");

        // ---------------------------------------------------------
        // Фрагмент 2. Сума замовлень, крім заблокованих клієнтів

        // Новий варіант: множина заблокованих id
        Console.WriteLine("--- Фрагмент 2 ---");
        var blockedSet = new HashSet<int>(blocked);
        int total = 0;
        foreach (var order in orders)
            if (!blockedSet.Contains(order.ClientId))
                total += order.Sum;
        Console.WriteLine($"Сума прийнятих замовлень: {total}");

        // ---------------------------------------------------------
        // Фрагмент 3. Скільки замовлень у кожного клієнта

        Console.WriteLine("--- Фрагмент 3 ---");
        var counts = new Dictionary<int, int>();
        foreach (var order in orders)
        {
            counts.TryGetValue(order.ClientId, out int c);
            counts[order.ClientId] = c + 1;
        }
        var printed = new HashSet<int>();
        foreach (var order in orders) // обходимо список, а не словник
        {
            if (!printed.Add(order.ClientId))
                continue;
            Console.WriteLine($"{order.ClientId}: {counts[order.ClientId]}");
        }

        // ---------------------------------------------------------
        // Фрагмент 4. Найбільше замовлення — БЕЗ ЗМІН
        // Один прохід, жодного пошуку за ключем: структура тут не потрібна.
        Console.WriteLine("--- Фрагмент 4 ---");
        var best = orders[0];
        foreach (var order in orders)
            if (order.Sum > best.Sum)
                best = order;
        Console.WriteLine($"Найбільше замовлення: {best.Sum}");

        // ---------------------------------------------------------
        // Фрагмент 5. «Оптимізований» варіант фрагмента 1 — БЕЗ ЗМІН

        Console.WriteLine("--- Фрагмент 5 ---");
        foreach (var order in orders)
        {
            var byId5 = new Dictionary<int, Client>();
            foreach (var client in clients)
                byId5[client.Id] = client;
            Console.WriteLine($"Замовлення {order.Number} — {byId5[order.ClientId].Name}");
        }

        // ---------------------------------------------------------
        // Частина 2. Перевірка
        Console.WriteLine("--- Перевірка ---");
        int withoutOrders = 0;
        foreach (var client in clients)
            if (!counts.ContainsKey(client.Id))
                withoutOrders++;
        Console.WriteLine($"Клієнтів без замовлень: {withoutOrders}");
    }
}