using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P013 - Đổi phút sang giờ phút
        int n = int.Parse(Console.ReadLine());
	int h = n/60;
	int phut=n%60;
	Console.WriteLine(h+" "+phut);
    }
}