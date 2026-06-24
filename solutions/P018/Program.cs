using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P018 - Khoảng cách hai điểm
        string[] arr = Console.ReadLine().Split();
	double x1 = double.Parse(arr[0]);  
	double y1 = double.Parse(arr[1]);    
	double x2 = double.Parse(arr[2]);
	double y2 = double.Parse(arr[3]);   
	double dx = x2 - x1;
	double dy = y2 - y1;
	double khoangCach = Math.Sqrt(dx * dx + dy * dy);
	Console.WriteLine(khoangCach.ToString("F2"));
    }
}