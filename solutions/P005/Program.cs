using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P005 - Diện tích hình tròn
	double r = double.Parse(Console.ReadLine());
	double bankinh=r*r*3.14;
	Console.WriteLine(bankinh.ToString("F2"));
    }
}