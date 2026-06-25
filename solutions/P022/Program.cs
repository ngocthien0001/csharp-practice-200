using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P022 - Số lớn hơn
        string [] array=Console.ReadLine().Split();
	int a=int.Parse(array[0]);
	int b=int.Parse(array[1]);
	if(a==b){Console.WriteLine("Equal");}
	if(a>b){
	Console.WriteLine(a);
}	if(b>a){
	Console.WriteLine(b);
}
    }
}