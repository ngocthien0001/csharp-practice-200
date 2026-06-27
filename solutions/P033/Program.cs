using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P033 - Loại tam giác
        string[] array = Console.ReadLine().Split();
        int a = int.Parse(array[0]);
        int b = int.Parse(array[1]);
        int c = int.Parse(array[2]);
        if (a + b <= c || a + c <= b || b + c <= a)
        {
            Console.WriteLine("Invalid");
        }
        else if (a == b && b == c)
        {
            Console.WriteLine("Equilateral");
        }
        else if (a == b || a == c || b == c)
        {
            Console.WriteLine("Isosceles");
        }
        else
        {
            Console.WriteLine("Scalene");
        }
    }
}