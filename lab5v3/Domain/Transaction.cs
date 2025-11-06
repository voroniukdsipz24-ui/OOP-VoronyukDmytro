using System;

namespace Domain
{
    public sealed class Transaction
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime Timestamp { get; }
        public TransactionType Type { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public Transaction(TransactionType type, decimal amount, string description)
        {
            Timestamp = DateTime.Now;
            Type = type;
            Amount = amount;
            Description = description;
        }
    }
}