int GCD(int a, int b)
{
  return b == 0 ? a : GCD(b, a % b);
}

Console.Write("Введите числитель: ");
int numerator = int.Parse(Console.ReadLine());

Console.Write("Введите знаменатель: ");
int denominator = int.Parse(Console.ReadLine());
if (denominator == 0)
{
  Console.WriteLine("Ошибка: знаменатель не может быть равен 0.");
}
else
{
  int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));

  // Упрощаем дробь
  numerator /= gcd;
  denominator /= gcd;

  if (denominator < 0)
  {
    numerator = -numerator;
    denominator = -denominator;
  }

  // Выводим результат
  Console.WriteLine($"Результат: {numerator} / {denominator}");
}
