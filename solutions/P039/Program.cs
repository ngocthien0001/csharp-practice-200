using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P039 - Thứ trong tuần
        int d = int.Parse(Console.ReadLine());
        switch (d)
        {
            case 1:
                Console.WriteLine("Monday");
                break;
            case 2:
                Console.WriteLine("Tuesday");
                break;
            case 3:
                Console.WriteLine("Wednesday");
                break;
            case 4:
                Console.WriteLine("Thursday");
                break;
            case 5:
                Console.WriteLine("Friday");
                break;
            case 6:
                Console.WriteLine("Saturday");
                break;
            case 7:
                Console.WriteLine("Sunday");
                break;
            default:
                Console.WriteLine("Invalid");
                break;
        }
    }
}