using System;

namespace OOP_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Figure f = new Figure(25.5);
            Console.WriteLine(f.GetFigure());

            f.Area = 40.2;
            Console.WriteLine(f.GetFigure());
        }
    }
}