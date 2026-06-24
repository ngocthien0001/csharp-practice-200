using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P014 - Đổi giây sang giờ phút giây
        int n = int.Parse(Console.ReadLine());
	int h = n / 3600; 
	int phut = (n % 3600) / 60;
	int giay = n % 60;
	Console.WriteLine(h + " " + phut + " " + giay);
    }
}