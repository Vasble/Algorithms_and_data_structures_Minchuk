using System;
 
var log = new List<string>();
 
string[] input =
{
    "Запуск системи",
    "Користувач увійшов",
    "Відкрито файл report.txt",
    "Файл збережено",
    "Користувач вийшов"
};
 
foreach (string line in input)
{
    log.Add(line);
}
 

for (int i = log.Count - 1; i >= 0; i--)
{
    Console.WriteLine(log[i]);
}