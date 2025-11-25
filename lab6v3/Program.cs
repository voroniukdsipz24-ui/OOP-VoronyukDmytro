using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_v3
{
    public delegate decimal MoneyOperation(decimal a, decimal b);

    // ====== EventArgs для події ======
    public sealed class BalanceChangedEventArgs : EventArgs
    {
        public decimal OldBalance { get; }
        public decimal NewBalance { get; }
        public string Reason { get; }

        public BalanceChangedEventArgs(decimal oldBalance, decimal newBalance, string reason)
        {
            OldBalance = oldBalance;
            NewBalance = newBalance;
            Reason = reason;
        }
    }

    public class BankAccount
    {
        public string Number { get; }
        public decimal Balance { get; private set; }

        // ====== Подія (events) + обробники =====
        public event EventHandler<BalanceChangedEventArgs>? BalanceChanged;

        public BankAccount(string number, decimal balance)
        {
            Number = number;
            Balance = balance;
        }

        public void Deposit(decimal amount, Action<decimal> onApplied)
        {
            var old = Balance;
            Balance += amount;

            onApplied?.Invoke(amount);

            BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(old, Balance, $"Deposit {amount:F2}"));
        }

        // Predicate<decimal>: булева перевірка
        public bool TryWithdraw(decimal amount, Predicate<decimal> canWithdraw, Action<decimal> onApplied)
        {
            if (!canWithdraw(amount)) return false;

            var old = Balance;
            Balance -= amount;

            onApplied?.Invoke(amount);

            BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(old, Balance, $"Withdraw {amount:F2}"));
            return true;
        }

        public override string ToString() => $"{Number} : {Balance:F2}";
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var accounts = new List<BankAccount>
            {
                new BankAccount("UA-1001", 1250.50m),
                new BankAccount("UA-1002",  200.00m),
                new BankAccount("UA-1003", 9999.99m),
                new BankAccount("UA-1004",  450.10m),
                new BankAccount("UA-1005",    0.00m),
            };

            accounts.ForEach(acc =>
            {
                acc.BalanceChanged += (sender, e) =>
                {
                    Console.WriteLine($"[EVENT] {((BankAccount)sender!).Number}: {e.OldBalance:F2} -> {e.NewBalance:F2} ({e.Reason})");
                };
            });

            Console.WriteLine("=== Початковий список рахунків ===");
            accounts.ForEach(a => Console.WriteLine(a)); // List + Action<T>

            MoneyOperation addFeeAnonymous = delegate (decimal balance, decimal fee)
            {
                return balance - fee;
            };

            // ЛЯМБДА-ВИРАЗ
            MoneyOperation addBonusLambda = (balance, bonus) => balance + bonus;

            Console.WriteLine("\n=== Делегати (власний) + анонімний метод + лямбда ===");
            Console.WriteLine($"UA-1001 після fee 10: {addFeeAnonymous(accounts[0].Balance, 10m):F2}");
            Console.WriteLine($"UA-1002 після bonus 25: {addBonusLambda(accounts[1].Balance, 25m):F2}");


            Func<BankAccount, string> format = acc => $"{acc.Number} : {acc.Balance:F2}"; // Func<T, TResult>
            Action<string> print = s => Console.WriteLine(s);                              // Action<T>
            Predicate<BankAccount> isPositive = acc => acc.Balance > 0;                    // Predicate<T>

            Console.WriteLine("\n=== Func/Action/Predicate ===");
            print("Тільки рахунки з позитивним балансом:");
            accounts.Where(a => isPositive(a))
                    .Select(format)
                    .ToList()
                    .ForEach(print);


            Console.WriteLine("\n=== Завдання 3 (LINQ + лямбди) ===");

            var avgBalance = accounts.Average(a => a.Balance);     // Average + lambda
            var minAccount = accounts.MinBy(a => a.Balance);       // MinBy + lambda

            print($"Середній баланс: {avgBalance:F2}");
            print($"Мінімальний баланс: {minAccount!.Number} : {minAccount.Balance:F2}");

            print("Форматований вивід усіх рахунків:");
            accounts.Select(a => $"{a.Number} : {a.Balance:F2}")
                    .ToList()
                    .ForEach(print);

            Console.WriteLine("\n=== Where/Select/OrderBy/Aggregate ===");

            var lines = accounts.Where(a => a.Balance > 300m)
                                .OrderByDescending(a => a.Balance)
                                .Select(format)
                                .ToList();

            print("Баланс > 300 (спадання):");
            lines.ForEach(print);

            var total = accounts.Select(a => a.Balance)
                                .Aggregate(0m, (acc, x) => acc + x);

            print($"Сума всіх балансів (Aggregate): {total:F2}");


            Console.WriteLine("\n=== Deposit/Withdraw (Action + Predicate) + EVENT ===");

            Action<decimal> onApplied = amount => Console.WriteLine($"Операція застосована: {amount:F2}");

            Predicate<decimal> canWithdraw = amount => amount > 0 && accounts[1].Balance >= amount;

            accounts[1].Deposit(100m, onApplied);
            bool ok = accounts[1].TryWithdraw(500m, canWithdraw, onApplied);

            print(ok ? "Зняття успішне" : "Зняття відхилено");
            print($"Поточний стан: {accounts[1]}");

            Console.WriteLine("\n=== Кінець ===");
        }
    }
}
