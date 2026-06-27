using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P035 - Máy tính đơn giản
        string[] array = Console.ReadLine().Split();
        int a = int.Parse(array[0]);
        string op = array[1];
        int b = int.Parse(array[2]);
        if (op == "+")
        {
            Console.WriteLine(a + b);
        }
        else if (op == "-")
        {
            Console.WriteLine(a - b);
        }
        else if (op == "*")
        {
            Console.WriteLine(a * b);
        }
        else if (op == "/")
        {
            double result = (double)a / b;
            Console.WriteLine(result.ToString("F2"));
        }
    }
}