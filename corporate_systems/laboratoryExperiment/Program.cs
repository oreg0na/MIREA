using System;

class Program
{
  static void Main()
  {
    Console.WriteLine("*********************************************************");
    Console.Write("Введите количество бактерий: ");
    int bacteria = int.Parse(Console.ReadLine());
    Console.Write("Введите количество антибиотика: ");
    int antibiotics = int.Parse(Console.ReadLine());
    int hour = 0;
    int antibioticEffect = 10;
    while (bacteria > 0 && antibiotics > 0)
    {
      hour++;
      bacteria *= 2;
      bacteria -= antibioticEffect;
      antibioticEffect--;
      if (antibioticEffect <= 0)
      {
        antibiotics--;
        antibioticEffect = 10;
      }
      if (bacteria < 0)
      {
        bacteria = 0;
      }
      Console.WriteLine($"После {hour} часа бактерий осталось {bacteria}");
    }
    while (bacteria > 0)
    {
      hour++;
      bacteria *= 2;
      Console.WriteLine($"После {hour} часа бактерий осталось {bacteria}");
    }
  }
}
