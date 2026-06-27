using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P032 - Kiểm tra tam giác
        string[] array = Console.ReadLine().Split();
        int a = int.Parse(array[0]);
        int b = int.Parse(array[1]);
        int c = int.Parse(array[2]);
        if (a + b > c && a + c > b && b + c > a)
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }
}