using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P010 - Chữ số cuối cùng
        int n = int.Parse(Console.ReadLine());

        
	int chuSoCuoi = n % 10;

        
	Console.WriteLine(chuSoCuoi);
    }
}