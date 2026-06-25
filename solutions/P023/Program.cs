using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P023 - Số lớn nhất trong ba số
        string [] array=Console.ReadLine().Split();
	int a=int.Parse(array[0]);
	int b=int.Parse(array[1]);
	int c=int.Parse(array[2]);
	if(a>=b&&a>=c)Console.WriteLine(a);
	else if(b>=a&&b>=c)Console.WriteLine(b);
	else if(c>=a&&c>=b)Console.WriteLine(c);
    }
}