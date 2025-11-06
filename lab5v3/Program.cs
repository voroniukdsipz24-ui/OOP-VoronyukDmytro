using System;
using System.Globalization;
using Domain;
using Common;
using Exceptions;

namespace Lab5v3
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Демо-дані
            var accA = new Account(
                accountNumber: "UA123-0001",
                ownerName: "Itachi Uchiha",
                dailyWithdrawalLimit: 3000m,       // Ліміт зняття/доба
                monthlyTransferOutLimit: 20000m    // Ліміт переказів/місяць
            );

            var accB = new Account(
                accountNumber: "UA123-0002",
                ownerName: "Naruto Uzumaki",
                dailyWithdrawalLimit: 2500m,
                monthlyTransferOutLimit: 15000m
            );

            // --- Базові операції ---
            accA.Deposit(10000m, "Стартовий внесок");
            accA.Withdraw(1200m, "Готівка на покупки");

            // Переказ з A -> B
            var transferRes = accA.TransferTo(accB, 2000m, "Переказ другу");
            if (!transferRes.IsSuccess)
                Console.WriteLine($"Помилка переказу: {transferRes.Error}");

            // --- Баланси (як у прикладі в README) ---
            PrintHeader("Баланс рахунків");
            PrintBalance(accA);
            PrintBalance(accB);

            // --- Перевищення добового ліміту ---
            try
            {
                accA.Withdraw(2500m, "Спроба перевищити добовий ліміт");
            }
            catch (LimitExceededExceptionV2 ex)
            {
                Console.WriteLine($"Ліміт: {ex.Message}");
            }

            // --- Спроба зняти більше, ніж є на балансі ---
            try
            {
                accA.Withdraw(100000m, "Спроба зняти понад баланс");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Недостатньо коштів: {ex.Message}");
            }

            // --- Некоректна сума ---
            try
            {
                accB.Deposit(0m, "Нульова сума");
            }
            catch (InvalidTransactionException ex)
            {
                Console.WriteLine($"Невалідна операція: {ex.Message}");
            }

            // Далі — усе, що стосується LINQ/підсумків (як було)

            // Обчислення за допомогою LINQ
            PrintHeader("Підсумки за день (сьогодні)");
            var today = DateTime.Today;
            Console.WriteLine($"A: знято за {today:yyyy-MM-dd} = {accA.GetDailyTotal(today, TransactionType.Withdrawal):N2}");
            Console.WriteLine($"A: переказ OUT за {today:yyyy-MM-dd} = {accA.GetDailyTotal(today, TransactionType.TransferOut):N2}");
            Console.WriteLine($"B: отримано переказів IN за {today:yyyy-MM-dd} = {accB.GetDailyTotal(today, TransactionType.TransferIn):N2}");

            PrintHeader("Підсумки за місяць");
            var now = DateTime.Now;
            var monthTotalOutA = accA.GetMonthlyTotal(now.Year, now.Month, TransactionType.TransferOut);
            var monthWithdrawA = accA.GetMonthlyTotal(now.Year, now.Month, TransactionType.Withdrawal);
            Console.WriteLine($"A: перекази OUT за {MonthName(now)} = {monthTotalOutA:N2} (ліміт: {accA.MonthlyTransferOutLimit:N2})");
            Console.WriteLine($"A: зняття готівки за {MonthName(now)} = {monthWithdrawA:N2}");

            // Відсоток успішних операцій (простий приклад з Result<T>)
            PrintHeader("Result<T> приклад");
            var avgOutRes = accA.TryAverage(t => t.Type is TransactionType.Withdrawal or TransactionType.TransferOut ? t.Amount : 0m);
            Console.WriteLine(avgOutRes.IsSuccess
                ? $"A: середнє навантаження «вихідних» операцій = {avgOutRes.Value:N2}"
                : $"A: обчислення середнього неуспішне: {avgOutRes.Error}");

            // Групування по днях: сума рухів (всі типи), приклад LINQ GroupBy
            PrintHeader("Групування по днях (A)");
            foreach (var g in accA.GroupByDayTotals())
            {
                Console.WriteLine($"{g.Date:yyyy-MM-dd}: Σ={g.Total:N2}, " +
                                  $"In={g.InTotal:N2}, Out={g.OutTotal:N2}");
            }

            Console.WriteLine("\nГотово.");
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('─', Math.Max(3, title.Length)));
            Console.WriteLine(title);
            Console.WriteLine(new string('─', Math.Max(3, title.Length)));
        }

        static void PrintBalance(Account a)
        {
            Console.WriteLine($"{a.AccountNumber} | {a.OwnerName} | Баланс: {a.GetBalance():N2}");
        }

        static string MonthName(DateTime dt)
            => CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat.GetMonthName(dt.Month);
    }
}
