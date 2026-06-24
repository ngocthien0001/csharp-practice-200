using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P015 - Lãi đơn
        string[] arr = Console.ReadLine().Split(); 
	decimal p = decimal.Parse(arr[0]); 
	decimal r = decimal.Parse(arr[1]);
	decimal y = decimal.Parse(arr[2]);
	decimal tienLai = p * r * y / 100;
	Console.WriteLine(tienLai.ToString("F2"));
    }
}