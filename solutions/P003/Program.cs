using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P003 - Tích hai số nguyên
        string [] arr = Console.ReadLine().Split();
	int a = int.Parse(arr[0]);
	int b = int.Parse(arr[1]);
	Console.WriteLine(a*b);
    }
}