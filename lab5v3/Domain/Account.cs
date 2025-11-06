using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Exceptions;

namespace Domain
{
    // Композиція: Account володіє колекцією Transaction (List<Transaction>)
    public sealed class Account
    {
        private readonly List<Transaction> _transactions = new();

        public string AccountNumber { get; }
        public string OwnerName { get; }
        public decimal DailyWithdrawalLimit { get; }
        public decimal MonthlyTransferOutLimit { get; }

        public IReadOnlyList<Transaction> Transactions => _transactions;

        public Account(string accountNumber, string ownerName,
                       decimal dailyWithdrawalLimit, decimal monthlyTransferOutLimit)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Порожній номер рахунку.", nameof(accountNumber));
            if (string.IsNullOrWhiteSpace(ownerName))
                throw new ArgumentException("Порожнє ім'я власника.", nameof(ownerName));
            if (dailyWithdrawalLimit <= 0)
                throw new ArgumentException("Добовий ліміт має бути > 0.", nameof(dailyWithdrawalLimit));
            if (monthlyTransferOutLimit <= 0)
                throw new ArgumentException("Місячний ліміт має бути > 0.", nameof(monthlyTransferOutLimit));

            AccountNumber = accountNumber;
            OwnerName = ownerName;
            DailyWithdrawalLimit = dailyWithdrawalLimit;
            MonthlyTransferOutLimit = monthlyTransferOutLimit;
        }

        // Поточний баланс (вхід мінус вихід)
        public decimal GetBalance()
        {
            var incoming = _transactions
                .Where(t => t.Type is TransactionType.Deposit or TransactionType.TransferIn)
                .Sum(t => t.Amount);

            var outgoing = _transactions
                .Where(t => t.Type is TransactionType.Withdrawal or TransactionType.TransferOut)
                .Sum(t => t.Amount);

            return incoming - outgoing;
        }

        // Валідація суми
        private static void EnsurePositive(decimal amount)
        {
            if (amount <= 0m)
                throw new InvalidTransactionException("Сума має бути додатною і більшою за нуль.");
        }

        // Поповнення
        public void Deposit(decimal amount, string description = "")
        {
            EnsurePositive(amount);
            _transactions.Add(new Transaction(TransactionType.Deposit, amount, description));
        }

        // Зняття готівки з перевіркою балансу та добового ліміту
        public void Withdraw(decimal amount, string description = "")
        {
            EnsurePositive(amount);

            // 1) Спочатку перевіряємо баланс
            if (amount > GetBalance())
                throw new InsufficientFundsException(
                    $"Запитано {amount:N2}, баланс {GetBalance():N2}.");

            // 2) Потім — добовий ліміт
            var today = DateTime.Today;
            var todayWithdrawn = _transactions
                .Where(t => t.Type == TransactionType.Withdrawal && t.Timestamp.Date == today)
                .Sum(t => t.Amount);

            if (todayWithdrawn + amount > DailyWithdrawalLimit)
                throw new LimitExceededExceptionV2(
                    $"Перевищено добовий ліміт зняття: {todayWithdrawn + amount:N2} > {DailyWithdrawalLimit:N2}.");

            _transactions.Add(new Transaction(TransactionType.Withdrawal, amount, description));
        }

        // Переказ між рахунками з перевіркою місячного ліміту OUT і балансу
        public Result<Transaction> TransferTo(Account target, decimal amount, string description = "")
        {
            if (target is null) return Result<Transaction>.Fail("Цільовий рахунок не задано.");
            EnsurePositive(amount);

            var now = DateTime.Now;
            var monthOut = _transactions
                .Where(t => t.Type == TransactionType.TransferOut &&
                            t.Timestamp.Year == now.Year && t.Timestamp.Month == now.Month)
                .Sum(t => t.Amount);

            if (monthOut + amount > MonthlyTransferOutLimit)
                return Result<Transaction>.Fail(
                    $"Перевищено місячний ліміт переказів OUT: {monthOut + amount:N2} > {MonthlyTransferOutLimit:N2}.");

            if (amount > GetBalance())
                return Result<Transaction>.Fail(
                    $"Недостатньо коштів: запитано {amount:N2}, баланс {GetBalance():N2}.");

            var outTx = new Transaction(TransactionType.TransferOut, amount, description);
            var inTx  = new Transaction(TransactionType.TransferIn, amount, $"Вхідний: {description}");

            _transactions.Add(outTx);
            target._transactions.Add(inTx);

            return Result<Transaction>.Ok(outTx);
        }

        // Денний підсумок за типом
        public decimal GetDailyTotal(DateTime day, TransactionType type)
            => _transactions.Where(t => t.Type == type && t.Timestamp.Date == day.Date)
                            .Sum(t => t.Amount);

        // Місячний підсумок за типом
        public decimal GetMonthlyTotal(int year, int month, TransactionType type)
            => _transactions.Where(t => t.Type == type &&
                                        t.Timestamp.Year == year &&
                                        t.Timestamp.Month == month)
                            .Sum(t => t.Amount);

        // Приклад: обчислити середнє по довільному селектору суми (LINQ + Result<T>)
        public Result<decimal> TryAverage(Func<Transaction, decimal> selector)
        {
            var seq = _transactions.Select(selector).ToList();
            if (seq.Count == 0)
                return Result<decimal>.Fail("Немає транзакцій для обчислення.");

            return Result<decimal>.Ok(seq.Average());
        }

        // Групування по днях із агрегатами
        public IEnumerable<(DateTime Date, decimal Total, decimal InTotal, decimal OutTotal)> GroupByDayTotals()
        {
            return _transactions
                .GroupBy(t => t.Timestamp.Date)
                .OrderBy(g => g.Key)
                .Select(g => (
                    Date: g.Key,
                    Total: g.Sum(t => t.Type is TransactionType.Deposit or TransactionType.TransferIn ? t.Amount : -t.Amount),
                    InTotal: g.Where(t => t.Type is TransactionType.Deposit or TransactionType.TransferIn).Sum(t => t.Amount),
                    OutTotal: g.Where(t => t.Type is TransactionType.Withdrawal or TransactionType.TransferOut).Sum(t => t.Amount)
                ));
        }
    }
}
