using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P016 - Chỉ số BMI
         string[] arr = Console.ReadLine().Split(); 
	decimal a = decimal.Parse(arr[0]); 
	decimal b = decimal.Parse(arr[1]);
	decimal bmi = a/(b*b);
	Console.WriteLine(bmi.ToString("F2"));
    }
}