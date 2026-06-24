using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P008 - Điểm trung bình
        string [] arr = Console.ReadLine().Split();
	double a = double.Parse(arr[0]);
	double b = double.Parse(arr[1]);
	double c = double.Parse(arr[2]);
	double tb = (a+b+c)/3;
	Console.WriteLine(tb.ToString("F2"));
    }
}