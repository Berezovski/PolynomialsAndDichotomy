using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PolynomialAndDichotomy
{
    class Program
    {
        // Программа не работает с огромными числами 
        static void Main(string[] args)
        {
            // лист корней
            SortedList<int, int> brackets = new SortedList<int, int>();
            // (x-4)^3 * (x-16) * (x - 21) = 0
            brackets.Add(4, 3);
            brackets.Add(16, 1);
            brackets.Add(21, 1);
            PrintPoly(brackets);

            double x;
            WorkWithPoly wp = new WorkWithPoly(brackets);
            double y = wp.StartFindMin(out x);

            Console.WriteLine("Min:");
            Console.WriteLine("x: " + x);
            Console.WriteLine("y: " + y);

            y = wp.StartFindMax(out x);

            Console.WriteLine("Max:");
            Console.WriteLine("x: " + x);
            Console.WriteLine("y: " + y);
        } 
        
        /// <summary>
        /// Выводим весь полином
        /// </summary>
        /// <param name="brackets"> Массив данных </param>
        static void PrintPoly(SortedList<int, int> brackets)
        {
            StringBuilder strBl = new StringBuilder();
            for (int i = 0; i < brackets.Count; i++)
            {
                if (brackets.Values[i] == 1)
                {
                    strBl.Append("(x - ").Append(brackets.Keys[i]).Append(")").Append(" * ");
                }
                else
                {
                    strBl.Append("(x - ").Append(brackets.Keys[i]).Append(")^").Append(brackets.Values[i]).Append(" * ");
                }
            }
            strBl.Remove(strBl.Length - 3, 3);
            strBl.Append(" = 0");
            Console.WriteLine(strBl.ToString());
        }

    }
}
