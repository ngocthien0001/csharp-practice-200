using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P038 - Giảm giá đơn hàng
        int total = int.Parse(Console.ReadLine());
        int pay;
        if (total >= 1000000)
        {
            pay = total - total * 10 / 100;
        }
        else if (total >= 500000)
        {
            pay = total - total * 5 / 100;
        }
        else
        {
            pay = total;
        }
        Console.WriteLine(pay);
    }
}