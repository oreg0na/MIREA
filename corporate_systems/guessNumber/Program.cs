int min = 0;
int max = 63;
int attempts = 0;

Console.WriteLine("Загадайте число от 0 до 63. Программа угадает его не более чем за 7 вопросов.");

while (min <= max && attempts < 7)
{
  int guess = (min + max) / 2;
  Console.WriteLine($"Ваше число больше {guess}? (да-1 / нет-0 / равно-2)");

  int answer = int.Parse(Console.ReadLine());

  if (answer == 1)
  {
    min = guess + 1;
  }
  else if (answer == 0)
  {
    max = guess - 1;
  }
  else if (answer == 2)
  {
    Console.WriteLine($"Число угадано! Это {guess}.");
    break;
  }
  attempts++;
}

if (attempts >= 7)
{
  Console.WriteLine("Превышено количество попыток.");
}
