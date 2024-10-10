using System;
using System.Globalization;

class Program
{
    static double Factorial(int n)
    {
        double result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    static double MaclaurinSeries(double x, double epsilon)
    {
        double sum = 1;
        double term = 1;
        int n = 1;

        while (true)
        {
            term = Math.Pow(x, n) / Factorial(n);
            if (Math.Abs(term) < epsilon)
                break;
            sum += term;
            n++;
        }

        return sum;
    }

    static double NthTerm(double x, int n)
    {
        return Math.Pow(x, n) / Factorial(n);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Введите значение x:");
        double x = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

        Console.WriteLine("Введите точность e (например, 0.01):");
        double epsilon = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

        double result = MaclaurinSeries(x, epsilon);
        Console.WriteLine($"Значение функции e^x для x = {x} с точностью {epsilon} равно: {result}");

        Console.WriteLine("Введите номер n для вычисления n-го члена ряда:");
        int n = Convert.ToInt32(Console.ReadLine());
        double nthTerm = NthTerm(x, n);
        Console.WriteLine($"n-й член ряда для x = {x} и n = {n} равен: {nthTerm}");
    }
}
