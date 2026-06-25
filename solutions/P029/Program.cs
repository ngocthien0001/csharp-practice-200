using System;
class Program
{
    static void Main()
    {
        // P029 - Xếp loại điểm
        double score = double.Parse(Console.ReadLine());
        if (score >= 8)
        {
            Console.WriteLine("Good");
        }
        else if (score >= 6.5)
        {
            Console.WriteLine("Fair");
        }
        else if (score >= 5)
        {
            Console.WriteLine("Average");
        }
        else
        {
            Console.WriteLine("Weak");
        }
    }
}