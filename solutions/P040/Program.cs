using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P040 - Số ngày trong tháng
        string[] array = Console.ReadLine().Split();
        int month = int.Parse(array[0]);
        int year = int.Parse(array[1]);
        if (month < 1 || month > 12)
        {
            Console.WriteLine("Invalid");
        }
        else if (month == 2)
        {
            if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
            {
                Console.WriteLine(29);
            }
            else
            {
                Console.WriteLine(28);
            }
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            Console.WriteLine(30);
        }
        else
        {
            Console.WriteLine(31);
        }
    }
}