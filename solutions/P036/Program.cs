using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P036 - Tính thuế thu nhập đơn giản
        long salary = long.Parse(Console.ReadLine());
        long tax;
        if (salary <= 10000000)
        {
            tax = 0;
        }
        else if (salary <= 20000000)
        {
            tax = (salary - 10000000) * 10 / 100;
        }
        else
        {
            tax = 10000000 * 10 / 100 + (salary - 20000000) * 20 / 100;
        }
        Console.WriteLine(tax);
    }
}