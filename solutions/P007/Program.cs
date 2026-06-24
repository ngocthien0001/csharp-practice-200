using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P007 - Đổi độ C sang độ F
	string C=Console.ReadLine();
	double F=double.Parse(C) * 9 / 5 + 32;
	Console.WriteLine(F.ToString("F1"));
    }
}