using System;
 
(int Month, decimal Amount)[] operations =
{
    (1, 1200m),
    (3, 450.50m),
    (1, 300m),
    (12, 999.99m),
    (7, 80m),
    (3, 49.50m),
    (7, 20m),
    (10, 5000m)
};
 
decimal[] totals = new decimal[12]; 
 
foreach (var op in operations)
{
    totals[op.Month - 1] += op.Amount; 
}
 
for (int month = 1; month <= 12; month++)
{
    Console.WriteLine($"Місяць {month,2}: {totals[month - 1]}");
}