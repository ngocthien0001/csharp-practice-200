using System;
using System.Linq;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // P037 - Đăng nhập đơn giản
        string username = Console.ReadLine();
        string password = Console.ReadLine();
        if (username == "admin" && password == "123456")
        {
            Console.WriteLine("Login success");
        }
        else
        {
            Console.WriteLine("Login failed");
        }
    }
}