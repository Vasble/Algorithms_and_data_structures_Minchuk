using System;

class Program
{
    static int comparisons = 0;

    static int[] data = { 42, 8, 60, 19, 3, 55, 12, 31, 68, 24, 49, 37, 71, 5, 27 };

    static int LinearSearch(int[] items, int target)
    {
        for (int i = 0; i < items.Length; i++)
        {
            comparisons++;

            if (items[i] == target)
                return i;
        }

        return -1;
    }

    static int BinarySearch(int[] items, int target)
    {
        int low = 0;
        int high = items.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            comparisons++;

            if (items[mid] == target)
                return mid;

            if (items[mid] < target)
                low = mid + 1;
            else
                high = mid - 1;
        }

        return -1;
    }

    static void Report(string name, int[] items, int target)
    {
        comparisons = 0;

        int index;

        if (name == "linear")
            index = LinearSearch(items, target);
        else
            index = BinarySearch(items, target);

        Console.WriteLine($"{name}: target = {target}, index = {index}, comparisons = {comparisons}");
    }

    static void Main()
    {
        int[] sortedData = (int[])data.Clone();
        Array.Sort(sortedData);

        Console.WriteLine("SORTED DATA:");
        Console.WriteLine(string.Join(", ", sortedData));

        Console.WriteLine("\nPART 2:");

        Report("linear", sortedData, 3);
        Report("binary", sortedData, 3);

        Report("linear", sortedData, 71);
        Report("binary", sortedData, 71);

        Report("linear", sortedData, 31);
        Report("binary", sortedData, 31);

        Report("linear", sortedData, 1);
        Report("binary", sortedData, 1);

        Report("linear", sortedData, 99);
        Report("binary", sortedData, 99);

        Report("linear", sortedData, 50);
        Report("binary", sortedData, 50);

        int[] oneElement = { 42 };

        Report("linear", oneElement, 42);
        Report("binary", oneElement, 42);

        int[] empty = { };

        Report("linear", empty, 42);
        Report("binary", empty, 42);

        Console.WriteLine("\nPART 3:");
        comparisons = 0;

        int result = BinarySearch(data, 55);

        Console.WriteLine($"Binary search in unsorted data: index = {result}");
        Console.WriteLine($"Comparisons = {comparisons}");
    }
}