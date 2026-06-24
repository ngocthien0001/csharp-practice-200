using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P019 - Chu vi tam giác
        string[] arr = Console.ReadLine().Split();
	int a = int.Parse(arr[0]);  
	int b = int.Parse(arr[1]);  
	int c = int.Parse(arr[2]);
	int chuVi = a + b + c;  
	Console.WriteLine(chuVi);
    }
}