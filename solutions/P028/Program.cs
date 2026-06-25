using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P028 - Năm nhuận
        int year=int.Parse(Console.ReadLine());
	if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
{
Console.WriteLine("Leap year");
}
        
	else
{Console.WriteLine("Common year");}
    }
}