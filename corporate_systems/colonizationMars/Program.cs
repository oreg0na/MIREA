using System;

class Program
{
  static void Main()
  {
    Console.WriteLine("*********************************************************");
    Console.Write("Введите n: ");
    int n = int.Parse(Console.ReadLine());
    Console.Write("Введите a: ");
    int a = int.Parse(Console.ReadLine());
    Console.Write("Введите b: ");
    int b = int.Parse(Console.ReadLine());
    Console.Write("Введите w: ");
    int w = int.Parse(Console.ReadLine());
    Console.Write("Введите h: ");
    int h = int.Parse(Console.ReadLine());
    int maxD = FindMaxD(n, a, b, w, h);
    Console.WriteLine($"Ответ d = {maxD}");
  }
  static int FindMaxD(int n, int a, int b, int w, int h)
  {
    int d = 0;
    while (true)
    {
      int moduleWidth = a + 2 * d;
      int moduleHeight = b + 2 * d;
      int modulesPerWidth = w / moduleWidth;
      int modulesPerHeight = h / moduleHeight;
      if (modulesPerWidth * modulesPerHeight < n)
      {
        return d - 1;
      }
      d++;
    }
  }
}
