public class Account
{
    public decimal Balance { get; private set; }

    public Account(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InvalidOperationException("Недостатньо коштів.");
        }
        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
}
