using System;
class Program
{
    static void Main()
    {
        // P030 - Giá trị tuyệt đối
        int n = int.Parse(Console.ReadLine());
        if (n < 0)
        {
            n = -n;
        }
        Console.WriteLine(n);
    }
}