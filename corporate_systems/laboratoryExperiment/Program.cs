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

    while (bacteria > 0 && antibiotics > 0 && hour < 10)
    {
      hour++;
      bacteria *= 2;
      bacteria -= antibiotics * antibioticEffect;
      if (bacteria <= 0)
      {
        Console.WriteLine($"После {hour} часа бактерий осталось 0");
        Console.WriteLine("Колония вымерла.");
        break;
      }

      Console.WriteLine($"После {hour} часа бактерий осталось {bacteria}");
      antibioticEffect--;
      if (antibioticEffect == 0)
      {
        antibiotics--;
        antibioticEffect = 10;
      }
    }

    if (bacteria > 0 && hour == 10)
    {
      Console.WriteLine("Колония прожила 10 часов.");
    }
  }
}
