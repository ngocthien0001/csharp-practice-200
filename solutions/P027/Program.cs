using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P027 - Chia hết cho 3 và 5
        int a=int.Parse(Console.ReadLine());
	if(a%3==0&&a%5==0)Console.WriteLine("YES");
	else Console.WriteLine("NO");
    }
}