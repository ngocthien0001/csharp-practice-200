using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P011 - Tổng chữ số số có hai chữ số
        int n = int.Parse(Console.ReadLine());
	int hangChuc = n / 10;      
	int hangDonVi = n % 10;        
	Console.WriteLine(hangChuc + hangDonVi);
    }
}