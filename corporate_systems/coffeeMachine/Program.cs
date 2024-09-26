int water, milk;
int americanoCount = 0, latteCount = 0, totalEarnings = 0;

Console.Write("Введите количество воды в мл: ");
water = int.Parse(Console.ReadLine());

Console.Write("Введите количество молока в мл: ");
milk = int.Parse(Console.ReadLine());

while (true)
{
  Console.WriteLine("Выберите напиток (1 — американо, 2 — латте, 0 — завершить): ");
  int choice = int.Parse(Console.ReadLine());

  if (choice == 0)
  {
    Console.WriteLine("*Отчёт*");
    Console.WriteLine($"Ингредиентов осталось: \nВода: {water} мл\nМолоко: {milk} мл");
    Console.WriteLine($"Кружек американо приготовлено: {americanoCount}");
    Console.WriteLine($"Кружек латте приготовлено: {latteCount}");
    Console.WriteLine($"Итого: {totalEarnings} рублей.");
    break;
  }

  if (choice == 1) // Американо
  {
    if (water >= 300)
    {
      water -= 300;
      americanoCount++;
      totalEarnings += 150;
      Console.WriteLine("Ваш американо готов.");
    }
    else
    {
      Console.WriteLine("Не хватает воды для американо.");
    }
  }
  else if (choice == 2) // Латте
  {
    if (water >= 30 && milk >= 270)
    {
      water -= 30;
      milk -= 270;
      latteCount++;
      totalEarnings += 170;
      Console.WriteLine("Ваш латте готов.");
    }
    else if (water < 30)
    {
      Console.WriteLine("Не хватает воды для латте.");
    }
    else
    {
      Console.WriteLine("Не хватает молока для латте.");
    }
  }

  Console.WriteLine($"Остаток ингредиентов: \nВода: {water} мл\nМолоко: {milk} мл");
  if (water < 30 && milk < 270)
  {
    Console.WriteLine("Ингредиентов недостаточно для приготовления напитков.");
    Console.WriteLine("*Отчёт*");
    Console.WriteLine($"Ингредиентов осталось: \nВода: {water} мл\nМолоко: {milk} мл");
    Console.WriteLine($"Кружек американо приготовлено: {americanoCount}");
    Console.WriteLine($"Кружек латте приготовлено: {latteCount}");
    Console.WriteLine($"Итого: {totalEarnings} рублей.");
    break;
  }
}
