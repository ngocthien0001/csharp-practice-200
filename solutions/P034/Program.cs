using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P034 - Góc phần tư
        string[] array = Console.ReadLine().Split();
        int x = int.Parse(array[0]);
        int y = int.Parse(array[1]);
        if (x == 0 || y == 0)
        {
            Console.WriteLine("Axis");
        }
        else if (x > 0 && y > 0)
        {
            Console.WriteLine("Q1");
        }
        else if (x < 0 && y > 0)
        {
            Console.WriteLine("Q2");
        }
        else if (x < 0 && y < 0)
        {
            Console.WriteLine("Q3");
        }
        else
        {
            Console.WriteLine("Q4");
        }
    }
}