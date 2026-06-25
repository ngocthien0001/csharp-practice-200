using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P026 - Âm dương hay bằng 0
        int a=int.Parse(Console.ReadLine());
	if(a>0)Console.WriteLine("Positive");
	else if(a==0)Console.WriteLine("Zero");
	else if(a<0)Console.WriteLine("Negative");
    }
}