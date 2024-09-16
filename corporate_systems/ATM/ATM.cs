using System;

class ATM {
  static void Main(){

    int[] denominations = { 5000, 2000, 1000, 500, 200, 100};
    Console.Write("Введите сумму для снятия (не более 150.000 рублей): ");
    int amount = int.Parse(Console.ReadLine());

    if (amount > 150000){
      Console.WriteLine("Ошибка! Максимальная сумма для снятия - 150.000 рублей");
      return;
    } else if (amount % 100 != 0){
      Console.WriteLine("Ошибка! Сумма должна быть кратна 100.");
      return;
    }

    Console.WriteLine("Для выдачи суммы {0} рублей потребуется:", amount);
    for (int i = 0; i < denominations.Length; i++){
      if (amount >= denominations[i]){
        int numOfNotes = amount / denominations[i];
        amount %= denominations[i];
        Console.WriteLine($"{numOfNotes} купюр(ы) по {denominations[i]} рублей");
      }
    }

    if (amount != 0){
      Console.WriteLine("Ошибка: невозможно выдать ровно запрашиваемую сумму.");
    }
  }
}
