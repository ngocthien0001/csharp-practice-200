using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P006 - Hình chữ nhật
        string [] arr = Console.ReadLine().Split();
	int a = int.Parse(arr[0]);
	int b = int.Parse(arr[1]);
	int chuvi=(a+b)*2;
	int dientich=a*b;
	Console.WriteLine(chuvi+"\n"+dientich);
    }
}