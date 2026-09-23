using System;
 
const int N = 5;
 
string[] buffer = new string[N];
int next = 0;   
int count = 0; 
 
string[] stream =
{
    "Подія 1", "Подія 2", "Подія 3", "Подія 4",
    "Подія 5", "Подія 6", "Подія 7", "Подія 8"
};
 
foreach (string ev in stream)
{
    buffer[next] = ev;
    next = (next + 1) % N;        
    if (count < N) count++;
}

int start = count < N ? 0 : next;
 
for (int i = 0; i < count; i++)
{
    Console.WriteLine(buffer[(start + i) % N]);
}