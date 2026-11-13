using System;
using System.Collections.Generic;

class Program
{
    // TODO 1
    static HashSet<string> VisitAll(Dictionary<string, List<string>> graph, string start)
    {
        var visited = new HashSet<string>();
        if (!graph.ContainsKey(start)) return visited;

        var queue = new Queue<string>();
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            foreach (string next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }
        return visited;
    }

    // TODO 2
    static bool Reachable(Dictionary<string, List<string>> graph, string from, string to)
    {
        return VisitAll(graph, from).Contains(to);
    }

    // TODO 3
    static List<string> FindPath(Dictionary<string, List<string>> graph, string from, string to)
    {
        var path = new List<string>();
        if (!graph.ContainsKey(from)) return path;

        var parent = new Dictionary<string, string>();
        var visited = new HashSet<string> { from };
        var queue = new Queue<string>();
        queue.Enqueue(from);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            foreach (string next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    parent[next] = current;
                    queue.Enqueue(next);
                }
            }
        }

        if (!visited.Contains(to)) return path;
        string v = to;
        path.Add(v);
        while (parent.ContainsKey(v))
        {
            v = parent[v];
            path.Add(v);
        }
        path.Reverse();
        return path;
    }

    static string Show(List<string> path)
    {
        return path.Count == 0 ? "[] (порожній список)" : "[" + string.Join(" → ", path) + "]";
    }

    static void Main()
    {
        var graph = new Dictionary<string, List<string>>();
        graph["Центр"]   = new List<string> { "Парк", "Ринок" };
        graph["Парк"]    = new List<string> { "Центр", "Вокзал" };
        graph["Ринок"]   = new List<string> { "Центр", "Вокзал", "Стадіон" };
        graph["Вокзал"]  = new List<string> { "Парк", "Ринок", "Озеро" };
        graph["Стадіон"] = new List<string> { "Ринок" };
        graph["Озеро"]   = new List<string> { "Вокзал" };
        graph["Гай"]     = new List<string> { "Міст" };
        graph["Міст"]    = new List<string> { "Гай" };

        var fromCenter = VisitAll(graph, "Центр");
        Console.WriteLine("Обхід: у ширину (BFS)");
        Console.WriteLine("visitAll(\"Центр\"): " + fromCenter.Count + " вершин — " + string.Join(", ", fromCenter));
        Console.WriteLine("reachable(\"Центр\", \"Озеро\"): " + Reachable(graph, "Центр", "Озеро"));
        Console.WriteLine("reachable(\"Центр\", \"Гай\"): " + Reachable(graph, "Центр", "Гай"));
        Console.WriteLine("reachable(\"Гай\", \"Міст\"): " + Reachable(graph, "Гай", "Міст"));

        var pLake = FindPath(graph, "Центр", "Озеро");
        Console.WriteLine("findPath(\"Центр\", \"Озеро\"): " + Show(pLake) + ", ребер: " + (pLake.Count - 1));

        var pStadium = FindPath(graph, "Центр", "Стадіон");
        Console.WriteLine("findPath(\"Центр\", \"Стадіон\"): " + Show(pStadium) + ", ребер: " + (pStadium.Count - 1));

        Console.WriteLine("findPath(\"Центр\", \"Міст\"): " + Show(FindPath(graph, "Центр", "Міст")));

        Console.WriteLine("Недосяжні з \"Центру\": " + (graph.Count - fromCenter.Count));
    }
}