using System;

class Calculator
{
  static double memory = 0;

  static void Main(string[] args)
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("Консольный калькулятор");
      Console.WriteLine("Выберите операцию:");
      Console.WriteLine("1 - Сложение (+)");
      Console.WriteLine("2 - Вычитание (-)");
      Console.WriteLine("3 - Умножение (*)");
      Console.WriteLine("4 - Деление (/)");
      Console.WriteLine("5 - Остаток от деления (%)");
      Console.WriteLine("6 - Обратное значение (1/x)");
      Console.WriteLine("7 - Возведение в квадрат (x^2)");
      Console.WriteLine("8 - Квадратный корень (√x)");
      Console.WriteLine("9 - Сохранить в память (M+)");
      Console.WriteLine("10 - Вычесть из памяти (M-)");
      Console.WriteLine("11 - Показать значение памяти (MR)");
      Console.WriteLine("12 - Выход");

      string choice = Console.ReadLine();

      if (choice == "12") break;

      double number1 = 0, number2 = 0;

      // Для операций, требующих одного числа
      if (choice == "6" || choice == "7" || choice == "8")
      {
        number1 = GetNumber("Введите число: ");
      }

      // Для операций, требующих двух чисел
      if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5")
      {
        number1 = GetNumber("Введите первое число: ");
        number2 = GetNumber("Введите второе число: ");
      }

      try
      {
        switch (choice)
        {
          case "1":
            Console.WriteLine($"Результат: {number1} + {number2} = {number1 + number2}");
            break;
          case "2":
            Console.WriteLine($"Результат: {number1} - {number2} = {number1 - number2}");
            break;
          case "3":
            Console.WriteLine($"Результат: {number1} * {number2} = {number1 * number2}");
            break;
          case "4":
            if (number2 == 0) throw new DivideByZeroException("Деление на ноль невозможно.");
            Console.WriteLine($"Результат: {number1} / {number2} = {number1 / number2}");
            break;
          case "5":
            if (number2 == 0) throw new DivideByZeroException("Остаток от деления на ноль невозможен.");
            Console.WriteLine($"Результат: {number1} % {number2} = {number1 % number2}");
            break;
          case "6":
            if (number1 == 0) throw new DivideByZeroException("Обратное значение для нуля не существует.");
            Console.WriteLine($"Результат: 1/{number1} = {1 / number1}");
            break;
          case "7":
            Console.WriteLine($"Результат: {number1}^2 = {Math.Pow(number1, 2)}");
            break;
          case "8":
            if (number1 < 0) throw new ArgumentException("Квадратный корень не может быть извлечен из отрицательного числа.");
            Console.WriteLine($"Результат: √{number1} = {Math.Sqrt(number1)}");
            break;
          case "9":
            memory += GetNumber("Введите число для добавления в память: ");
            Console.WriteLine($"Число добавлено в память. Текущая память: {memory}");
            break;
          case "10":
            memory -= GetNumber("Введите число для вычитания из памяти: ");
            Console.WriteLine($"Число вычтено из памяти. Текущая память: {memory}");
            break;
          case "11":
            Console.WriteLine($"Значение памяти: {memory}");
            break;
          default:
            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            break;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка: {ex.Message}");
      }

      Console.WriteLine("\nНажмите любую клавишу для продолжения...");
      Console.ReadKey();
    }
  }

  static double GetNumber(string prompt)
  {
    double result;
    while (true)
    {
      Console.Write(prompt);
      if (double.TryParse(Console.ReadLine(), out result)) break;
      Console.WriteLine("Ошибка: введено недопустимое число.");
    }
    return result;
  }
}
