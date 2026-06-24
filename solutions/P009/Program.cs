using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P009 - Hoán đổi hai số
        string [] arr = Console.ReadLine().Split();
	int a = int.Parse(arr[0]);
	int b = int.Parse(arr[1]);
	Console.WriteLine(b+" "+a);
    }
}