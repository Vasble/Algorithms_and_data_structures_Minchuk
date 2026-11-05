
using System;

class Node
{
    public int Value;
    public Node Left;   
    public Node Right;

    public Node(int value)
    {
        Value = value;
    }
}

class Program
{
    static Node root = null;
    static int[] values = { 42, 18, 65, 9, 27, 55, 90, 33 }; 
    static int compareCount = 0; 

    // TODO 1: 
    static Node Insert(Node node, int key)
    {
        if (node == null)            
            return new Node(key);

        if (key < node.Value)
            node.Left = Insert(node.Left, key);
        else if (key > node.Value)
            node.Right = Insert(node.Right, key);

        return node;
    }

    // TODO 2: 
    static bool Search(Node node, int key)
    {
        if (node == null)             
            return false;

        compareCount++;              
        if (key == node.Value)
            return true;
        if (key < node.Value)
            return Search(node.Left, key);
        return Search(node.Right, key);
    }

    // TODO 3:
    static void Inorder(Node node)
    {
        if (node == null)
            return;

        Inorder(node.Left);           
        Console.Write(node.Value + " "); 
        Inorder(node.Right);        
    }

    static int CountNodes(Node node)
    {
        if (node == null) return 0;
        return 1 + CountNodes(node.Left) + CountNodes(node.Right);
    }

    static int Height(Node node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    static void RunSearch(int key)
    {
        compareCount = 0;             
        bool found = Search(root, key);
        Console.WriteLine($"Search(root, {key}) = {(found ? "так" : "ні")}, порівнянь: {compareCount}");
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        foreach (int v in values)
            root = Insert(root, v);

        Console.WriteLine("Вузлів у дереві після восьми вставок: " + CountNodes(root));

        Console.Write("Симетричний обхід: ");
        Inorder(root);
        Console.WriteLine();

        RunSearch(55);
        RunSearch(30);

        Console.WriteLine("Глибина найдовшої гілки: " + Height(root));

        root = Insert(root, 27);       
        Console.WriteLine("Вузлів після повторної вставки 27: " + CountNodes(root));

        root = null;
        foreach (int v in new[] { 9, 18, 27, 33, 42, 55, 65, 90 })
            root = Insert(root, v);
        Console.WriteLine("\n[вироджене дерево] глибина: " + Height(root));
        RunSearch(90);
    }
}