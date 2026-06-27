using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // P031 - Sắp xếp ba số
        string[] array = Console.ReadLine().Split();
        int a = int.Parse(array[0]);
        int b = int.Parse(array[1]);
        int c = int.Parse(array[2]);
        int[] nums = { a, b, c };
        Array.Sort(nums);
        Console.WriteLine(nums[0] + " " + nums[1] + " " + nums[2]);
    }
}