using System;

Console.Write("Введите номер билета (6 цифр): ");
int ticketNumber = int.Parse(Console.ReadLine());

int firstPart = ticketNumber / 1000;
int firstSum = (firstPart / 100) + (firstPart / 10 % 10) + (firstPart % 10);

int secondPart = ticketNumber % 1000;
int secondSum = (secondPart / 100) + (secondPart / 10 % 10) + (secondPart % 10);

if (firstSum == secondSum)
{
  Console.WriteLine("True");
}
else
{
  Console.WriteLine("False");
}