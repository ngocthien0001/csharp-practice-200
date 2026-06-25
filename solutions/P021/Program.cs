using System;
class Program
{
    static void Main()
    {
        // P021 - Tiền điện đơn giản
        int kwh = int.Parse(Console.ReadLine());
        int tien;
        if (kwh <= 50)
        {
            tien = kwh * 1800;
        }
        else
        {
            tien = 50 * 1800 + (kwh - 50) * 2500;
        }
        Console.WriteLine(tien);
    }
}