using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P020 - Số lon sơn cần mua
        double area = double.Parse(Console.ReadLine());        
	int soLon = (int)Math.Ceiling(area / 10);       
	Console.WriteLine(soLon);
    }
}