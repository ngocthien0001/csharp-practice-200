using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P012 - Tổng chữ số số có ba chữ số
        int n = int.Parse(Console.ReadLine());
	int hangtram = n / 100;
	int hangchuc = (n / 10)%10;      
	int hangdonvi = n % 10;        
	Console.WriteLine(hangtram + hangchuc + hangdonvi);
    }
}