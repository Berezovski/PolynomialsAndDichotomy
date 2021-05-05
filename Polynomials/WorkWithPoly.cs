using System;
using System.Collections.Generic;
using System.Text;

namespace PolynomialAndDichotomy
{
    class WorkWithPoly
    {
        // лист для скобок (x-int)^int* ...
        private SortedList<int, int> _brackets;
        private const double _epsilon = 0.001;
        private const double _lessEpsilon = 0.00001;

        /// <summary>
        /// Перенимает значения brackets (одновременно проверяя их)
        /// </summary>
        /// <param name="brackets"> лист для скобок (x-int)^int* ... </param>
        public WorkWithPoly(SortedList<int, int> brackets)
        {
            CheckBrackets(brackets);
             _brackets = brackets;
        }

        /// <summary>
        /// Проверка введённого полинома
        /// </summary>
        /// <param name="brackets"> Данные в виде множителей в скобках </param>
        private void CheckBrackets(SortedList<int, int> brackets)
        {
            if (brackets.Count < 2)
            {
                throw new ArgumentException("Array.Length < 2");
            }

            for (int i = 0; i < brackets.Count; i++)
            {
                if ((brackets.Values[i] < 1) || (brackets.Values[i] > 2021))
                {
                    throw new ArgumentException("1 < Array value < 2021");
                }
                if ((brackets.Keys[i] < 1) || (brackets.Keys[i] > 2021))
                {
                    throw new ArgumentException("1 < Array key < 2021");
                }
            }
        }

        /// <summary>
        /// Ищем минимальный y
        /// </summary>
        /// <param name="x"> x минимального значения y</param>
        /// <returns> Минимальный y </returns>
        public double StartFindMin(out double x)
        {
            // минимальные значения на отрезках
            SortedList<double,double> checkArrayForAnswer = new SortedList<double, double>();
            for (int i = 0; i < _brackets.Count - 1; i++)
            {
                double isX;
                double isY = DichotomyForMin(_brackets.Keys[i], _brackets.Keys[i + 1], out isX);
                checkArrayForAnswer.Add(isX, isY);
            }

            double y = checkArrayForAnswer.Values[0];
            // сравниваем корни для минимального значения
            for (int i = 0; i < checkArrayForAnswer.Count; i++)
            { 
                if (y > checkArrayForAnswer.Values[i])
                {
                    y = checkArrayForAnswer.Values[i];
                }
            }

            x = checkArrayForAnswer.Keys[checkArrayForAnswer.Values.IndexOf(y)];
            return Math.Round(y);
        }

        /// <summary>
        /// Ищем максимальный y
        /// </summary>
        /// <param name="x"> x максимальный значения y</param>
        /// <returns> Максимальный y </returns>
        public double StartFindMax(out double x)
        {
            // максимальные значения на отрезках
            SortedList<double, double> checkArrayForAnswer = new SortedList<double, double>();
            for (int i = 0; i < _brackets.Count - 1; i++)
            {
                double isX;
                double isY = DichotomyForMax(_brackets.Keys[i], _brackets.Keys[i + 1], out isX);
                checkArrayForAnswer.Add(isX, isY);
            }

            double y = checkArrayForAnswer.Values[0];
            // сравниваем корни для максимального значения
            for (int i = 0; i < checkArrayForAnswer.Count; i++)
            {
                if (y < checkArrayForAnswer.Values[i])
                {
                    y = checkArrayForAnswer.Values[i];
                }
            }

            x = checkArrayForAnswer.Keys[checkArrayForAnswer.Values.IndexOf(y)];
            return Math.Round(y);
        }

        /// <summary>
        /// Применяет дихотомию на отрезке
        /// </summary>
        /// <param name="a"> левая часть отрезка  </param>
        /// <param name="b"> правая часть отрезка </param>
        /// <param name="x"> x максимального значения y на отрезке </param>
        /// <returns> Максимальное значение y </returns>
        private double DichotomyForMax(double a, double b, out double x)
        {
            double p;
            double q;

            while (b - a > _epsilon)
            {
                p = (b + a) / 2 - _lessEpsilon;
                q = (b + a) / 2 + _lessEpsilon;

                if (FindValue(p) > FindValue(q))
                {
                    b = q;
                }
                else
                {
                    a = p;
                }

            }

            x = (b + a) / 2;
            return FindValue(x);

        }

        /// <summary>
        /// Применяет дихотомию на отрезке
        /// </summary>
        /// <param name="a"> левая часть отрезка  </param>
        /// <param name="b"> правая часть отрезка </param>
        /// <param name="x"> x минимального значения y на отрезке </param>
        /// <returns> Минимальное значение y </returns>
        private double DichotomyForMin(double a, double b, out double x)
        {
            double p;
            double q;

            while (b - a > _epsilon)
            {
                p = (b + a) / 2 - _lessEpsilon;
                q = (b + a) / 2 + _lessEpsilon;

                if (FindValue(p) < FindValue(q))
                {
                    b = q;
                }
                else
                {
                    a = p;
                }

            }

            x = (b + a) / 2;
            return FindValue(x);

        }

        /// <summary>
        /// Вычисляет значение по x 
        /// </summary>
        /// <param name="x"> x </param>
        /// <returns> f(y) </returns>
        private double FindValue(double x)
        {
            double answer = 1;
            for (int i = 0; i < _brackets.Count; i++)
            {
                answer = answer * Math.Pow((x - _brackets.Keys[i]), _brackets.Values[i]);
            }
            return answer;
        }
    }
}
