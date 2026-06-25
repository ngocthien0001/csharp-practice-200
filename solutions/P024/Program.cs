using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P024 - Số nhỏ nhất trong bốn số
         string [] array=Console.ReadLine().Split();
	int a=int.Parse(array[0]);
	int b=int.Parse(array[1]);
	int c=int.Parse(array[2]);
	int d=int.Parse(array[3]);
	if(a<=b&&a<=c&&a<=d)Console.WriteLine(a);
	else if(b<=a&&b<=c&&b<=d)Console.WriteLine(b);
	else if(c<=a&&c<=b&&c<=d)Console.WriteLine(c);
	else if(d<=a&&d<=b&&d<=c)Console.WriteLine(d);
    }
}