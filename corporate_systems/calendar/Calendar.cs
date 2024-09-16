using System;

class Calendar {
  static void Main(){
    Console.Write("Введите номер дня недели, с которого начинается месяц (1-пн, 7-вс): ");
    int startDayOfWeek = int.Parse(Console.ReadLine());
    if (startDayOfWeek < 1 || startDayOfWeek > 7){
      Console.WriteLine("Неккоректный номер дня недели. Введите значение от 1 до 7.");
      return;
    }

    Console.Write("Введите день месяца: ");
    int day = int.Parse(Console.ReadLine());

    if (day < 1 || day > 31){
      Console.WriteLine("Неккоректный номер дня месяца. Введите значение от 1 до 31.");
      return;
    }

    string[] weekDays = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

    int dayOfWeek = (startDayOfWeek + (day - 1) % 7 - 1) % 7;
    bool isWeekend = dayOfWeek == 5 || dayOfWeek == 6; // Суббота и воскресенье
    bool isHoliday = (day >= 1 && day <= 5) || (day >= 8 && day <= 10);

    Console.WriteLine("----- Проверяем, выходной ли день -----");
    if (isWeekend || isHoliday){
      Console.WriteLine("Выходной день");
    } else {
      Console.WriteLine("Рабочий день");
    }
  }
}
